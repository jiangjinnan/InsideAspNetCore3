using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar>(_ => new Bar())
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider();
            var provider1 = root.CreateScope().ServiceProvider;
            var provider2 = root.CreateScope().ServiceProvider;           

            GetServices<IFoo>(provider1);
            GetServices<IBar>(provider1);
            GetServices<IBaz>(provider1);
            Console.WriteLine();
            GetServices<IFoo>(provider2);
            GetServices<IBar>(provider2);
            GetServices<IBaz>(provider2);

            static void GetServices<T>(IServiceProvider provider)
            {
                provider.GetService<T>();
                provider.GetService<T>();
            }
        }
    }
}
