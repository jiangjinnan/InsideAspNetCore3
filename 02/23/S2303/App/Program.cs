using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var random = new Random();
            var options = new HealthCheckOptions
            {
                ResultStatusCodes = new Dictionary<HealthStatus, int>
                {
                    [HealthStatus.Healthy] = 299,
                    [HealthStatus.Degraded] = 298,
                    [HealthStatus.Unhealthy] = 503
                }
            };
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddHealthChecks().AddCheck("default", Check))
                    .Configure(app => app.UseHealthChecks("/healthcheck", options)))
                .Build()
                .Run();

            HealthCheckResult Check()
            {
                return (random.Next(1, 4)) switch
                {
                    1 => HealthCheckResult.Unhealthy(),
                    2 => HealthCheckResult.Degraded(),
                    _ => HealthCheckResult.Healthy(),
                };
            }
        }
    }
}