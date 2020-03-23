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
    public static void Main()
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(builder => builder
            .ConfigureServices(svcs => svcs.AddRouting())
            .Configure(app => app
                .UseExceptionHandler("/error")
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGet("error", HandleErrorAsync))
                .Run(context => Task.FromException(new InvalidOperationException("Manually thrown exception")))))
            .Build()
            .Run();

        static async Task HandleErrorAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            var ex = context.Features.Get<IExceptionHandlerPathFeature>().Error;

            await context.Response.WriteAsync("<html><head><title>Error</title></head><body>");
            await context.Response.WriteAsync($"<h3>{ex.Message}</h3>");
            await context.Response.WriteAsync($"<p>Type: {ex.GetType().FullName}");
            await context.Response.WriteAsync($"<p>StackTrace: {ex.StackTrace}");
            await context.Response.WriteAsync("</body></html>");
        }
    }
}
}