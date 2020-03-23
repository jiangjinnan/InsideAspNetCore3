using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace App
{
    class Program
    {
        static void Main()
        {
            File.AppendAllLines("log.csv", new string[] { $"EventName, Payload, ActivityId, RelatedActivityId" });
            _ = new LoggingEventListener();
            var logger = new ServiceCollection()
                .AddLogging(builder => builder.AddEventSourceLogger())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            using (logger.BeginScope(new Dictionary<string, object> { ["Operation"] = "Foo" }))
            {
                logger.LogInformation("This is a test log written in scope 'Foo'");
                using (logger.BeginScope(new Dictionary<string, object> { ["Operation"] = "Bar" }))
                {
                    logger.LogInformation("This is a test log written in scope 'Bar'");
                }
                using (logger.BeginScope(new Dictionary<string, object>{ ["Operation"] = "Baz" }))
                {
                    logger.LogInformation("This is a test log written in scope 'Baz'");
                }
            }
        }
    }
}
