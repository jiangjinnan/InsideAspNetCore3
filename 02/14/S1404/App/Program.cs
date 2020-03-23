using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "doc");
            var fileProvider = new PhysicalFileProvider(path);
            var fileOptions = new StaticFileOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };
            var diretoryOptions = new DirectoryBrowserOptions
            {
                FileProvider = fileProvider,
                RequestPath = "/documents"
            };
            var defaultOptions = new DefaultFilesOptions
            {
                RequestPath = "/documents",
                FileProvider = fileProvider,
            };

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseDefaultFiles()
                    .UseDefaultFiles(defaultOptions)
                    .UseStaticFiles()
                    .UseStaticFiles(fileOptions)
                    .UseDirectoryBrowser()
                    .UseDirectoryBrowser(diretoryOptions)))
                .Build()
                .Run();
        }
    }
}