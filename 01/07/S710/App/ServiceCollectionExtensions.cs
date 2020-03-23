using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.IO;

namespace App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Configure<TOptions>( this IServiceCollection services, string filePath, string basePath = null) where TOptions : class, new()
        => services.Configure<TOptions>(Options.DefaultName, filePath, basePath);

        public static IServiceCollection Configure<TOptions>(this IServiceCollection services, string name, string filePath, string basePath = null) where TOptions : class, new()
        {
            var fileProvider = string.IsNullOrEmpty(basePath)
                ? new PhysicalFileProvider(Directory.GetCurrentDirectory())
                : new PhysicalFileProvider(basePath);

            return services.AddSingleton<IConfigureOptions<TOptions>>(new JsonFileConfigureOptions<TOptions>(name, filePath, fileProvider));
        }
    }
}
