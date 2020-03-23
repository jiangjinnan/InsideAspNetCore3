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
            var defaultOptions1 = new DefaultFilesOptions();
            var defaultOptions2 = new DefaultFilesOptions
            {
                RequestPath = "/documents",
                FileProvider = fileProvider,
            };

            defaultOptions1.DefaultFileNames.Add("readme.html");
            defaultOptions2.DefaultFileNames.Add("readme.html");

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseDefaultFiles(defaultOptions1)
                    .UseDefaultFiles(defaultOptions2)
                    .UseStaticFiles()
                    .UseStaticFiles(fileOptions)
                    .UseDirectoryBrowser()
                    .UseDirectoryBrowser(diretoryOptions)))
                .Build()
                .Run();
        }
    }
}