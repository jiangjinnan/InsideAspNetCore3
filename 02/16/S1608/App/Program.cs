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
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddSingleton<IDeveloperPageExceptionFilter, FakeExceptionFilter>())
                    .Configure(app => app
                        .UseDeveloperExceptionPage()
                        .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception...")))))
                .Build()
                .Run();
        }

        private class FakeExceptionFilter : IDeveloperPageExceptionFilter
        {
            public Task HandleExceptionAsync(ErrorContext errorContext, Func<ErrorContext, Task> next)
                => errorContext.HttpContext.Response.WriteAsync("Unhandled exception occurred!");
        }
    }
}