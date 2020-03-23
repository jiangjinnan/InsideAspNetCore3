using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace App
{
    public static class Extensions
    {
        public static IHealthChecksBuilder AddConsolePublisher(this IHealthChecksBuilder builder)
        {
            builder.Services.AddSingleton<IHealthCheckPublisher, ConsolePublisher>();
            return builder;
        }

        public static IHealthChecksBuilder ConfigurePublisher(this IHealthChecksBuilder builder, Action<HealthCheckPublisherOptions> configure)
        {
            builder.Services.Configure(configure);
            return builder;
        }
    }
}
