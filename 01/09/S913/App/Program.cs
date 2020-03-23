using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main()
        {
            _ = new LoggingEventListener();
            var logger = new ServiceCollection()
                .AddLogging(builder => builder.AddEventSourceLogger())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            var state = new Dictionary<string, object>
            {
                ["ErrorCode"] = 100,
                ["Message"] = "Unhandled exception"
            };

            logger.Log(LogLevel.Error, 1, state, new InvalidOperationException("This is a manually thrown exception."), (_, ex) => ex.Message);
            Console.Read();

        }
    }

}
