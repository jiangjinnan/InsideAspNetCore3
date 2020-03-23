using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics.Tracing;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Configuration;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("logging.json")
                .Build();

            var loggerFactory = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddConfiguration(configuration)
                    .AddConsole()
                    .AddDebug())
                .BuildServiceProvider()
                .GetRequiredService<ILoggerFactory>();

            var fooLogger = loggerFactory.CreateLogger("Foo");
            var barLogger = loggerFactory.CreateLogger("Bar");
            var bazLogger = loggerFactory.CreateLogger("Baz");

            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(it => it != LogLevel.None).ToArray();

            var eventId = 1;
            Array.ForEach(levels, level => fooLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

            eventId = 1;
            Array.ForEach(levels, level => barLogger.Log(level, eventId++, "This is a/an {0} log message.", level));

            eventId = 1;
            Array.ForEach(levels, level => bazLogger.Log(level, eventId++, "This is a/an {0} log message.", level));
            Console.Read();
        }
    }
}
