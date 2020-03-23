using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddMemoryCache())
                    .Configure(app => app.Run(ProocessAsync)))
                .Build()
                .Run();

            static async Task ProocessAsync(HttpContext httpContext)
            {
                var cache = httpContext.RequestServices.GetRequiredService<IMemoryCache>();
                if (!cache.TryGetValue<DateTime>("CurrentTime", out var currentTime))
                {
                    cache.Set("CurrentTime", currentTime = DateTime.Now);
                }
                await httpContext.Response.WriteAsync($"{currentTime}({DateTime.Now})");
            }
        }
    }
}