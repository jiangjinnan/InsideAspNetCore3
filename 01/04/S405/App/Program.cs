using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            using (var root = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddScoped<IBar, Bar>()
                .AddSingleton<IBaz, Baz>()
                .BuildServiceProvider())
            {
                using (var scope = root.CreateScope())
                {
                    var provider = scope.ServiceProvider;
                    provider.GetService<IFoo>();
                    provider.GetService<IBar>();
                    provider.GetService<IBaz>();
                    Console.WriteLine("Child container is disposed.");
                }
                Console.WriteLine("Root container is disposed.");
            }
        }
    }

}
