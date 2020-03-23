using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System;
using Microsoft.Extensions.Logging;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(svcs => svcs
                    .AddSingleton<IFoo, Foo>()
                    .AddScoped<IBar, Bar>()
                    .AddTransient<IBaz, Baz>()
                    .AddControllersWithViews())
                .Configure(app => app
                    .Use(next => httpContext => {
                        Console.WriteLine($"Receive request to {httpContext.Request.Path}");
                        return next(httpContext);
                    })
                    .UseRouting()
                    .UseEndpoints(endpoints => endpoints.MapControllers())))
            .ConfigureLogging(builder => builder.ClearProviders())
            .Build()
            .Run();
        }
    }

}
