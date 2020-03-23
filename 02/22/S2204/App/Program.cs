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
                        .UseRequestLocalization(options => options
                            .AddSupportedCultures("en", "zh")
                            .AddSupportedUICultures("en", "zh"))
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapControllers())))
                .Build()
                .Run();
        }
    }
}
