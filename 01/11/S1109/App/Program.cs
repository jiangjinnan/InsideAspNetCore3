using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureServices(svcs => svcs
                    .AddSingleton<IFoo, Foo>()
                    .AddSingleton<IBar, Bar>()
                    .AddControllersWithViews())
                .Configure(app => app
                    .UseRouting()
                    .UseEndpoints(endpoints => endpoints.MapControllers())))
            .Build()
            .Run();
        }
    }
}
