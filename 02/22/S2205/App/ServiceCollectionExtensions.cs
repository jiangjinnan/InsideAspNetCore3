using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJsonLocalizer(this IServiceCollection services, IFileProvider fileProvider)
        {
            services.Replace(ServiceDescriptor.Singleton<IStringLocalizerFactory>(
                new JsonStringLocalizerFactory(fileProvider)));
            return services;
        }
    }

}
