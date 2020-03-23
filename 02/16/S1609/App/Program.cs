using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddRouting()
                        .AddControllersWithViews()
                        .AddRazorRuntimeCompilation())
                    .Configure(app => app
                        .UseDeveloperExceptionPage()
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
            .Build()
            .Run();
        }
    }
}