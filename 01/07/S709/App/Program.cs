using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

namespace App
{
    class Program
    {
        static void Main()
        {
            var random = new Random();
            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<FoobarOptions>(foobar =>
                {
                    foobar.Foo = random.Next(1, 100);
                    foobar.Bar = random.Next(1, 100);
                })
                .BuildServiceProvider();

            Print(serviceProvider);
            Print(serviceProvider);

            static void Print(IServiceProvider provider)
            {
                var scopedProvider = provider
                    .GetRequiredService<IServiceScopeFactory>()
                    .CreateScope()
                    .ServiceProvider;

                var options = scopedProvider
                    .GetRequiredService<IOptions<FoobarOptions>>()
                    .Value;
                var optionsSnapshot1 = scopedProvider
                    .GetRequiredService<IOptionsSnapshot<FoobarOptions>>()
                    .Value;
                var optionsSnapshot2 = scopedProvider
                    .GetRequiredService<IOptionsSnapshot<FoobarOptions>>()
                    .Value;
                Console.WriteLine($"options:{options}");
                Console.WriteLine($"optionsSnapshot1:{optionsSnapshot1}");
                Console.WriteLine($"optionsSnapshot2:{optionsSnapshot2}\n");
            }
        }
    }
}
