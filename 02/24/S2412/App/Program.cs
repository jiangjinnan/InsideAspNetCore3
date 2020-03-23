using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UsePathBase("/foo")
                    .Map("/bar", appBuilder => appBuilder.Run(ProcessAsync))))
                .Build()
                .Run();
        }

        private static Task ProcessAsync(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;
            var url = $"{request.Scheme}://{request.Host}{request.PathBase}{request.Path}";
            return response.WriteAsync($"Url: {url}\nPathBase: {request.PathBase}\nPath: {request.Path}");
        }
    }
}
