using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace App
{
class Program
{
    static void Main()
    {
        var serviceProvider = new ServiceCollection()
            .AddSingleton<SingletonService>()
            .AddScoped<ScopedService>()
            .BuildServiceProvider();
        var rootScope = serviceProvider.GetService<IServiceProvider>();
        using (var scope = serviceProvider.CreateScope())
        {
            var child = scope.ServiceProvider;
            var singletonService = child.GetRequiredService<SingletonService>();
            var scopedService = child.GetRequiredService<ScopedService>();

            Debug.Assert(ReferenceEquals(child, child.GetRequiredService<IServiceProvider>()));
            Debug.Assert(ReferenceEquals(child, scopedService.RequestServices));
            Debug.Assert(ReferenceEquals(rootScope, singletonService.ApplicationServices));
        }
    }            

    public class SingletonService
    {
        public IServiceProvider ApplicationServices { get; }
        public SingletonService(IServiceProvider serviceProvider) => ApplicationServices = serviceProvider;
    }

    public class ScopedService
    {
        public IServiceProvider RequestServices { get; }
        public ScopedService(IServiceProvider serviceProvider) => RequestServices = serviceProvider;
    }
}
}
