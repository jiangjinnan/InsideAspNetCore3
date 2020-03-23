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
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddHealthChecks()
                        .AddCheck("foo", Check)
                        .AddCheck("bar", Check)
                        .AddCheck("baz", Check))
                    .Configure(app => app.UseHealthChecks("/healthcheck")))
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