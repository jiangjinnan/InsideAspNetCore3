using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddDistributedRedisCache(options => options.Configuration = "localhost")
                        .AddSession())
                    .Configure(app => app
                        .UseSession()
                        .Run(ProcessAsync)))
                .Build()
                .Run();

            static async Task ProcessAsync(HttpContext context)
            {
                var session = context.Session;
                await session.LoadAsync();
                string sessionStartTime;
                if (session.TryGetValue("SessionStartTime", out var value))
                {
                    sessionStartTime = Encoding.UTF8.GetString(value);
                }
                else
                {
                    sessionStartTime = DateTime.Now.ToString();
                    session.SetString("SessionStartTime", sessionStartTime);
                }

                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync($"<html><body><ul><li>Session ID:{session.Id}</li>");
                await context.Response.WriteAsync($"<li>Session Start Time:{sessionStartTime}</li>");
                await context.Response.WriteAsync($"<li>Current Time:{DateTime.Now}</li></ul></table></body></html>");
            }
        }
    }
}