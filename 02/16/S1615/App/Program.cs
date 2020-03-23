using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddRouting())
                    .Configure(app => app
                        .UseStatusCodePagesWithReExecute("/error/{0}")
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapGet("error/{statuscode}", HandleAsync))
                        .Run(ProcessAsync)))
                .Build()
                .Run();
            static async Task HandleAsync(HttpContext context)
            {
                var statusCode = context.GetRouteData().Values["statuscode"];
                await context.Response.WriteAsync($"Error occurred ({statusCode})");
            }
            static Task ProcessAsync(HttpContext context)
            {
                context.Response.StatusCode = _random.Next(400, 599);
                return Task.CompletedTask;
            }
        }
    }
}