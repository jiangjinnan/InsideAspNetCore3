using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(svcs => svcs.AddSingleton<IStartupFilter, FooStartupFilter>())
                .Configure(app => app
                    .UseMiddleware<BarMiddleware>()
                    .Run(context => context.Response.WriteAsync("...=>"))))
            .Build()
            .Run();
        }
    }
}
