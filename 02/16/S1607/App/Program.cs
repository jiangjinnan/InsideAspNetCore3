using Microsoft.AspNetCore.Builder;
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
                .ConfigureWebHostDefaults(webBuilder => webBuilder.Configure(app => app
                    .UseStatusCodePages(app2 => app2.Run(HandleAsync))
                    .Run(context => Task.Run(() => context.Response.StatusCode = _random.Next(400, 599)))))
                .Build()
                .Run();

            static async Task HandleAsync(HttpContext context)
            {
                var response = context.Response;
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