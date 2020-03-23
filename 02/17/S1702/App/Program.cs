using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace App
{
public class Program
{
    public static void Main()
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(svcs => svcs.AddDistributedRedisCache(options =>
                {
                    options.Configuration = "localhost";
                    options.InstanceName = "Demo";
                }))
                .Configure(app => app.Run(ProocessAsync)))
            .Build()
            .Run();

        static async Task ProocessAsync(HttpContext httpContext)
        {
            var cache = httpContext.RequestServices.GetRequiredService<IDistributedCache>();
            var currentTime = await cache.GetStringAsync("CurrentTime");
            if (null == currentTime)
            {
                currentTime = DateTime.Now.ToString();
                await cache.SetAsync("CurrentTime", Encoding.UTF8.GetBytes(currentTime));
            }
            await httpContext.Response.WriteAsync($"{currentTime}({DateTime.Now})");
        }
    }
}
}