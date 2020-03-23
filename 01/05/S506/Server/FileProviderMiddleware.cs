using HttpFileSystem;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Server
{
    public class FileProviderMiddleware
    {
        private readonly IFileProvider _fileProvider;

        public FileProviderMiddleware(RequestDelegate next, string root)
        {
            _fileProvider = new PhysicalFileProvider(root);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Query.ContainsKey("dir-meta"))
            {
                var dirContents = _fileProvider.GetDirectoryContents(context.Request.Path);
                var dirDecriptor = new HttpDirectoryContentsDescriptor(dirContents, CreatePhysicalPathResolver(context, true));
                await context.Response.WriteAsync(JsonConvert.SerializeObject(dirDecriptor));
            }
            else if (context.Request.Query.ContainsKey("file-meta"))
            {
                var fileInfo = _fileProvider.GetFileInfo(context.Request.Path);
                var fileDescriptor = new HttpFileDescriptor(fileInfo, CreatePhysicalPathResolver(context, false));
                await context.Response.WriteAsync(JsonConvert.SerializeObject(fileDescriptor));
            }
            else
            {
                await context.Response.SendFileAsync(_fileProvider.GetFileInfo(context.Request.Path));
            }
        }

        private Func<string, string> CreatePhysicalPathResolver(HttpContext context, bool isDirRequest)
        {
            string schema = context.Request.IsHttps ? "https" : "http";
            string host = context.Request.Host.Host;
            int port = context.Request.Host.Port ?? 8080;
            string pathBase = context.Request.PathBase.ToString().Trim('/');
            string path = context.Request.Path.ToString().Trim('/');

            pathBase = string.IsNullOrEmpty(pathBase) ? string.Empty : $"/{pathBase}";
            path = string.IsNullOrEmpty(path) ? string.Empty : $"/{path}";

            return isDirRequest
                ? (Func<string, string>)(name => $"{schema}://{host}:{port}{pathBase}{path}/{name}")
                : name => $"{schema}://{host}:{port}{pathBase}{path}";
        }
    }
}
