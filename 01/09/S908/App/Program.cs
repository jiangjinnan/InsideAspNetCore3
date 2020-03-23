using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static async Task Main()
        {
            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddConsole(options => options.IncludeScopes = true))
                    .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            using (logger.BeginScope($"Foobar Transaction[{Guid.NewGuid()}]"))
            {
                var stopwatch = Stopwatch.StartNew();
                await Task.Delay(500);
                logger.LogInformation("Operation foo completes at {0}", stopwatch.Elapsed);

                await Task.Delay(300);
                logger.LogInformation("Operation bar completes at {0}", stopwatch.Elapsed);

                await Task.Delay(800);
                logger.LogInformation("Operation baz completes at {0}", stopwatch.Elapsed);
            }
            Console.Read();
        }
    }
}
