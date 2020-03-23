using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization.Routing;
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
                        .AddRouting())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(endpoints => endpoints.MapGet("{ui-culture}", app.New()
                            .UseRequestLocalization(options => options
                                .AddSupportedCultures("zh", "en")
                                .AddSupportedUICultures("zh", "en")
                                .RequestCultureProviders.Insert(1, new RouteDataRequestCultureProvider()))
                            .UseMiddleware<FoobarMiddleware>()
                            .Build()))))
                .Build()
                .Run();
        }
    }
}
