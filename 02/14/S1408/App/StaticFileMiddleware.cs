using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class StaticFileMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly StaticFileOptions _options;

        public StaticFileMiddleware(RequestDelegate next, IWebHostEnvironment env, IOptions<StaticFileOptions> options)
        {
            _next = next;
            _options = options.Value;
            _options.FileProvider = _options.FileProvider ?? env.WebRootFileProvider;
            _options.ContentTypeProvider = _options.ContentTypeProvider?? new FileExtensionContentTypeProvider();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (this.TryGetFileInfo(context, out var contentType, out var fileInfo, out var lastModified, out var etag))
            {
                var preconditionState = GetPreconditionState(context, lastModified.Value, etag);
                await SendResponseAsync(preconditionState, context, etag,lastModified.Value, contentType, fileInfo);
                return;
            }
            await _next(context);
        }

        public bool TryGetFileInfo(HttpContext context, out string contentType, out IFileInfo fileInfo, out DateTimeOffset? lastModified, out EntityTagHeaderValue etag)
        {
            contentType = null;
            fileInfo = null;

            if (context.UseMethods("GET", "HEAD") && context.TryGetSubpath(_options.RequestPath, out var subpath) && _options.TryGetContentType(subpath, out contentType) && _options.TryGetFileInfo(subpath, out fileInfo))
            {
                var last = fileInfo.LastModified;
                long etagHash = last.ToFileTime() ^ fileInfo.Length;
                etag = new EntityTagHeaderValue('\"' + Convert.ToString(etagHash, 16) + '\"');
                lastModified = new DateTimeOffset(last.Year, last.Month, last.Day, last.Hour, last.Minute, last.Second, last.Offset).ToUniversalTime();
                return true;
            }

            etag = null;
            lastModified = null;
            return false;
        }

        private PreconditionState GetPreconditionState(HttpContext context, DateTimeOffset lastModified, EntityTagHeaderValue etag)
        {
            PreconditionState ifMatch, ifNonematch, ifModifiedSince, ifUnmodifiedSince;
            ifMatch = ifNonematch = ifModifiedSince = ifUnmodifiedSince = PreconditionState.Unspecified;

            var requestHeaders = context.Request.GetTypedHeaders();
            //If-Match:ShouldProcess or PreconditionFailed
            if (requestHeaders.IfMatch != null)
            {
                ifMatch = requestHeaders.IfMatch.Any(it => it.Equals(EntityTagHeaderValue.Any) || it.Compare(etag, true)) ? PreconditionState.ShouldProcess : PreconditionState.PreconditionFailed;
            }

            //If-None-Match:NotModified or ShouldProcess
            if (requestHeaders.IfNoneMatch != null)
            {
                ifNonematch = requestHeaders.IfNoneMatch.Any(it => it.Equals(EntityTagHeaderValue.Any) || it.Compare(etag, true)) ? PreconditionState.NotModified : PreconditionState.ShouldProcess;
            }

            //If-Modified-Since: ShouldProcess or NotModified
            if (requestHeaders.IfModifiedSince.HasValue)
            {
                ifModifiedSince = requestHeaders.IfModifiedSince < lastModified ? PreconditionState.ShouldProcess : PreconditionState.NotModified;
            }

            //If-Unmodified-Since: ShouldProcess or PreconditionFailed
            if (requestHeaders.IfUnmodifiedSince.HasValue)
            {
                ifUnmodifiedSince = requestHeaders.IfUnmodifiedSince > lastModified ? PreconditionState.ShouldProcess : PreconditionState.PreconditionFailed;
            }

            //Return maximum.
            return new PreconditionState[] {
            ifMatch, ifNonematch, ifModifiedSince, ifUnmodifiedSince }.Max();
        }

        private bool TryGetRanges(HttpContext context, DateTimeOffset lastModified, EntityTagHeaderValue etag, long length, out IEnumerable<RangeItemHeaderValue> ranges)
        {
            ranges = null;
            var requestHeaders = context.Request.GetTypedHeaders();

            //Check If-Range
            var ifRange = requestHeaders.IfRange;
            if (ifRange != null)
            {
                bool ignore = (ifRange.EntityTag != null && !ifRange.EntityTag.Compare(etag, true)) || (ifRange.LastModified.HasValue && ifRange.LastModified < lastModified);
                if (ignore)
                {
                    return false;
                }
            }

            var list = new List<RangeItemHeaderValue>();
            foreach (var it in requestHeaders.Range.Ranges)
            {
                //Range:{from}-{to} Or {from}-
                if (it.From.HasValue)
                {
                    if (it.From.Value < length - 1)
                    {
                        long to = it.To.HasValue ? Math.Min(it.To.Value, length - 1) : length - 1;
                        list.Add(new RangeItemHeaderValue(it.From.Value, to));
                    }
                }
                //Range：-{size}
                else if (it.To.Value != 0)
                {
                    long size = Math.Min(length, it.To.Value);
                    list.Add(new RangeItemHeaderValue(length - size, length - 1));
                }
            }
            return (ranges = list) != null;
        }

     
        private async Task SendResponseAsync(PreconditionState state, HttpContext context, EntityTagHeaderValue etag, DateTimeOffset lastModified, string contentType, IFileInfo fileInfo)
        {
            switch (state)
            {
                //304 Not Modified
                case PreconditionState.NotModified:
                    {
                        context.SetResponseHeaders(304, etag, lastModified, contentType,
                            fileInfo.Length);
                        break;
                    }
                //416 Precondition Failded
                case PreconditionState.PreconditionFailed:
                    {
                        context.SetResponseHeaders(412, etag, lastModified, contentType, fileInfo.Length);
                        break;
                    }
                case PreconditionState.Unspecified:
                case PreconditionState.ShouldProcess:
                    {
                        //200 OK
                        if (context.UseMethods("HEAD"))
                        {
                            context.SetResponseHeaders(200, etag, lastModified, contentType, fileInfo.Length);
                            return;
                        }

                        IEnumerable<RangeItemHeaderValue> ranges;
                        if (context.IsRangeRequest() && this.TryGetRanges(context, lastModified, etag, fileInfo.Length, out ranges))
                        {
                            RangeItemHeaderValue range = ranges.FirstOrDefault();
                            //416 
                            if (null == range)
                            {
                                context.SetResponseHeaders(416, etag, lastModified, contentType, fileInfo.Length);
                                return;
                            }
                            else
                            {
                                //206 Partial Content
                                context.SetResponseHeaders(206, etag, lastModified, contentType, fileInfo.Length, range);
                                context.Response.GetTypedHeaders().ContentRange = new ContentRangeHeaderValue(range.From.Value, range.To.Value, fileInfo.Length);
                                using (var stream = fileInfo.CreateReadStream())
                                {
                                    stream.Seek(range.From.Value, SeekOrigin.Begin);
                                    await StreamCopyOperation.CopyToAsync(stream, context.Response.Body, range.To - range.From + 1, context.RequestAborted);
                                }
                                return;
                            }
                        }
                        //200 OK
                        context.SetResponseHeaders(200, etag, lastModified, contentType, fileInfo.Length);
                        using (Stream stream = fileInfo.CreateReadStream())
                        {
                            await StreamCopyOperation.CopyToAsync(stream, context.Response.Body, fileInfo.Length, context.RequestAborted);
                        }
                        break;
                    }
            }
        }


        private enum PreconditionState
        {
            Unspecified = 0,
            NotModified = 1,
            ShouldProcess = 2,
            PreconditionFailed = 3,
        }

    }
}
