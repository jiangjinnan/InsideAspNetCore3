using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    static class Program
    {
        static void Main()
        {
            Log<Foo>();
            Log<Foo.Bar>();
            Log<Baz<Foo>>();
            Console.Read();

            static void Log<T>()
            {
                new ServiceCollection()
                  .AddLogging(builder => builder.AddConsole())
                  .BuildServiceProvider()
                  .GetRequiredService<ILogger<T>>()
                  .LogInformation($"{typeof(T).FullName}");
                Task.Delay(1).Wait();
            }
        }
    }
}
