using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var random = new Random();
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureLogging(logging => logging.ClearProviders())
                    .ConfigureServices(svcs => svcs.AddHealthChecks()
                        .AddCheck("foo", Check)
                        .AddCheck("bar", Check)
                        .AddCheck("baz", Check)
                        .AddConsolePublisher()
                        .ConfigurePublisher(options =>
                            options.Period = TimeSpan.FromSeconds(5)))
                    .Configure(app => app.UseHealthChecks("/healthcheck")))
                .Build()
                .Run();

            HealthCheckResult Check()
            {
                switch (random.Next(1, 4))
                {
                    case 1: return HealthCheckResult.Unhealthy();
                    case 2: return HealthCheckResult.Degraded();
                    default: return HealthCheckResult.Healthy();
                }
            }
        }
    }

}