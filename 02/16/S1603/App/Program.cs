using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler(app2 => app2.Run(HandleAsync))
                    .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
                .Build()
                .Run();

            static Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");
        }
    }
}