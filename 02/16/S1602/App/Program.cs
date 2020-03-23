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
            var options = new ExceptionHandlerOptions { ExceptionHandler = HandleAsync };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                    .UseExceptionHandler(options)
                    .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
                .Build()
                .Run();

            Task HandleAsync(HttpContext context)
                => context.Response.WriteAsync("Unhandled exception occurred!");
        }
    }
}