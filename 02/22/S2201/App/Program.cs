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
                        .AddLocalization()
                        .AddRouting()
                        .AddControllers())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
                .Build()
                .Run();
        }
    }

}
