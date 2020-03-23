using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public static class Extensions
    {
        public static bool UseMethods(this HttpContext context, params string[] methods)
            => methods.Contains(context.Request.Method, StringComparer.OrdinalIgnoreCase);

        public static bool TryGetSubpath(this HttpContext context, string requestPath, out PathString subpath)
           => new PathString(context.Request.Path).StartsWithSegments(requestPath, out subpath);

        public static bool TryGetContentType(this StaticFileOptions options, PathString subpath, out string contentType)
           => options.ContentTypeProvider.TryGetContentType(subpath.Value, out contentType) || (!string.IsNullOrEmpty(contentType = options.DefaultContentType) && options.ServeUnknownFileTypes);

        public static bool TryGetFileInfo(this StaticFileOptions options, PathString subpath, out IFileInfo fileInfo)
           => (fileInfo = options.FileProvider.GetFileInfo(subpath.Value)).Exists;

        public static bool IsRangeRequest(this HttpContext context)
            => context.Request.GetTypedHeaders().Range != null;

        public static void SetResponseHeaders(this HttpContext context, int statusCode, EntityTagHeaderValue etag, DateTimeOffset lastModified, string contentType, long contentLength, RangeItemHeaderValue range = null)
        {
            context.Response.StatusCode = statusCode;
            var responseHeaders = context.Response.GetTypedHeaders();
            if (statusCode < 400)
            {
                responseHeaders.ETag = etag;
                responseHeaders.LastModified = lastModified;
                context.Response.ContentType = contentType;
                context.Response.Headers[HeaderNames.AcceptRanges] = "bytes";
            }
            if (statusCode == 200)
            {
                context.Response.ContentLength = contentLength;
            }

            if (statusCode == 416)
            {
                responseHeaders.ContentRange = new ContentRangeHeaderValue(contentLength);
            }

            if (statusCode == 206 && range != null)
            {
                responseHeaders.ContentRange = new ContentRangeHeaderValue(range.From.Value, range.To.Value, contentLength);
            }
        }
    }
}
