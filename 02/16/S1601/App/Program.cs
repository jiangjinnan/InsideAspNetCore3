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
            .ConfigureServices(svcs => svcs.AddRouting())
            .ConfigureWebHostDefaults(builder => builder.Configure(app => app
                .UseDeveloperExceptionPage()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGet("{foo}/{bar}", HandleAsync))))
            .Build()
            .Run();

            static Task HandleAsync(HttpContext httpContext)
                => Task.FromException(new InvalidOperationException("Manually thrown exception..."));
        }
    }
}