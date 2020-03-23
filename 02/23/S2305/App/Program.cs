using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var random = new Random();
            var options = new HealthCheckOptions
            {
                ResponseWriter = ReportAsync
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddHealthChecks()
                        .AddCheck("foo", Check, new string[] { "foo1", "foo2" })
                        .AddCheck("bar", Check, new string[] { "bar1", "bar2" })
                        .AddCheck("baz", Check, new string[] { "baz1", "baz2" }))
                    .Configure(app => app.UseHealthChecks("/healthcheck", options)))
                .Build()
                .Run();

            static Task ReportAsync(HttpContext context, HealthReport report)
            {
                context.Response.ContentType = "application/json";
                var settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.Converters.Add(new StringEnumConverter());
                return context.Response.WriteAsync(JsonConvert.SerializeObject(report, settings));
            }

            HealthCheckResult Check()
            {
                return (random.Next(1, 4)) switch
                {
                    1 => HealthCheckResult.Unhealthy("Unavailable"),
                    2 => HealthCheckResult.Degraded("Degraded"),
                    _ => HealthCheckResult.Healthy("Normal"),
                };
            }
        }
    }
}