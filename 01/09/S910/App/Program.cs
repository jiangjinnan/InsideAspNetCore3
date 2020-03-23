using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static async Task Main()
        {
            var logger = new ServiceCollection()
                    .AddLogging(builder => builder
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole(options => options.IncludeScopes = true))
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            var scopeFactory = LoggerMessage.DefineScope<Guid>("Foobar Transaction[{TransactionId}]");
            var operationCompleted = LoggerMessage.Define<string, TimeSpan>(LogLevel.Trace, 3721, "Operation {operation} completes at {time}");

            using (scopeFactory(logger, Guid.NewGuid()))
            {
                await InvokeAsync();
            }

            using (scopeFactory(logger, Guid.NewGuid()))
            {
                await InvokeAsync();
            }

            Console.Read();

            async Task InvokeAsync()
            {
                var stopwatch = Stopwatch.StartNew();
                await Task.Delay(500);
                operationCompleted(logger, "foo", stopwatch.Elapsed, null);

                await Task.Delay(300);
                operationCompleted(logger, "bar", stopwatch.Elapsed, null);

                await Task.Delay(800);
                operationCompleted(logger, "baz", stopwatch.Elapsed, null);
            }
        }
    }

}
