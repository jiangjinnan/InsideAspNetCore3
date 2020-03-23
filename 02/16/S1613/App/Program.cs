using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseStatusCodePages(HandleAsync)
                    .Run(ProcessAsync)))
                .Build()
                .Run();

            static Task HandleAsync(StatusCodeContext context)
                => context.HttpContext.Response.WriteAsync("Error occurred!");

            static Task ProcessAsync(HttpContext context)
            {
                context.Response.StatusCode = 401;
                if (_random.Next() % 2 == 0)
                {
                    context.Features.Get<IStatusCodePagesFeature>().Enabled = false;
                }
                return Task.CompletedTask;
            }
        }
    }
}