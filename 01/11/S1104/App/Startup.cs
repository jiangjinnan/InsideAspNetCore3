using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            foreach (var service in services)
            {
                var serviceTypeName = GetName(service.ServiceType);
                var implementationType = service.ImplementationType
                        ?? service.ImplementationInstance?.GetType()
                        ?? service.ImplementationFactory?.Invoke(provider)?.GetType();
                if (implementationType != null)
                {
                    Console.WriteLine($"{service.Lifetime,-15}{ GetName(service.ServiceType),-50}{ GetName(implementationType)}");
                }
            }
        }

        public void Configure(IApplicationBuilder app) { }

        private string GetName(Type type)
        {
            if (!type.IsGenericType)
            {
                return type.Name;
            }
            var name = type.Name.Split('`')[0];
            var args = type.GetGenericArguments().Select(it => it.Name);
            return $"{name}<{string.Join(",", args)}>";
        }
    }

}