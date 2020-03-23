using App.Properties;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var template = "resources/{lang:culture}/{resourceName:required}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddRouting(options => options.ConstraintMap
                           .Add("culture", typeof(CultureConstraint))))
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(routes => routes.MapGet(
                            template, BuildHandler(routes.CreateApplicationBuilder())))))
                .Build()
                .Run();
            static RequestDelegate BuildHandler(IApplicationBuilder app)
            {
                app.UseMiddleware<LocalizationMiddleware>("lang")
                    .Run(async context =>
                    {
                        var values = context.GetRouteData().Values;
                        var resourceName = values["resourceName"].ToString().ToLower();
                        await context.Response.WriteAsync(
                        Resources.ResourceManager.GetString(resourceName));
                    });
                return app.Build();
            }
        }
    }

}