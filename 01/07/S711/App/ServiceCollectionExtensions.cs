using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, TimeSpan refreshInterval)
           => services.AddSingleton<IOptionsChangeTokenSource<TOptions>>(new TimedRefreshTokenSource<TOptions>(refreshInterval, name));
        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, TimeSpan refreshInterval)
           => services.Configure<TOptions>(Options.DefaultName, refreshInterval);
    }
}