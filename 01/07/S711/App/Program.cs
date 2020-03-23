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
            var optionsMonitor = new ServiceCollection()
                .AddOptions()
                .Configure<FoobarOptions>(TimeSpan.FromSeconds(1))
                .Configure<FoobarOptions>(foobar =>
                {
                    foobar.Foo = random.Next(10, 100);
                    foobar.Bar = random.Next(10, 100);
                })
                .BuildServiceProvider()
                .GetRequiredService<IOptionsMonitor<FoobarOptions>>();

            optionsMonitor.OnChange(foobar => Console.WriteLine($"[{DateTime.Now}]{foobar}"));
            Console.Read();
        }
    }

}
