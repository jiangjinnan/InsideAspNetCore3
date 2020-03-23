using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics;

namespace App
{
    public class Program
    {
        private static readonly Random _random = new Random();
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.Configure(app => app
                    .UseStatusCodePages(HandleAsync)
                    .Run(context => Task.Run(() => context.Response.StatusCode = _random.Next(400, 599)))))
                .Build()
                .Run();

            static async Task HandleAsync(StatusCodeContext context)
            {
                var response = context.HttpContext.Response;
                if (response.StatusCode < 500)
                {
                    await response.WriteAsync($"Client error ({response.StatusCode})");
                }
                else
                {
                    await response.WriteAsync($"Server error ({response.StatusCode})");
                }
            }
        }
    }
}