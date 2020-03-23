using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics.Tracing;

namespace App
{
public class Program
{
    public static void Main()
    {
        var loggerFactory = new ServiceCollection()
            .AddLogging(builder => builder
                .AddFilter(Filter)
                .AddConsole())
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

        static bool Filter(string category, LogLevel level)
        {
            return category switch
            {
                "Foo" => level >= LogLevel.Debug,
                "Bar" => level >= LogLevel.Warning,
                "Baz" => level >= LogLevel.None,
                _ => level >= LogLevel.Information,
            };
        }
    }
}

}
