using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        private static readonly Random _random = new Random();
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler(app2 => app2.Run(HandleAsync))
                    .Run(ProcessAsync)))
            .Build()
            .Run();

            static Task HandleAsync(HttpContext context) => context.Response.WriteAsync("Error occurred!");

            static async Task ProcessAsync(HttpContext context)
            {
                context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
                {
                    MaxAge = TimeSpan.FromHours(1)
                };

                if (_random.Next() % 2 == 0)
                {
                    throw new InvalidOperationException("Manually thrown exception...");
                }
                await context.Response.WriteAsync("Succeed...");
            }
        }
    }
}