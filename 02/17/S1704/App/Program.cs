using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Net.Http.Headers;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddResponseCaching())
                    .Configure(app => app
                        .UseResponseCaching()
                        .Run(ProcessAsync)))
                .Build()
                .Run();

            static async Task ProcessAsync(HttpContext httpContext)
            {
                var response = httpContext.Response;
                response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(3600)
                };
                var isUtc = httpContext.Request.Query.ContainsKey("utc");
                await response.WriteAsync(isUtc ? DateTime.UtcNow.ToString() : DateTime.Now.ToString());
            }
        }
    }
}