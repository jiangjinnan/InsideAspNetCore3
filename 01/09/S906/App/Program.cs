using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Diagnostics.Tracing;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace App
{
public class Program
{
    public static void Main()
    {
        var logger = new ServiceCollection()
            .AddLogging(builder => builder
                .AddFilter(Filter)
                .AddConsole()
                .AddDebug())
            .BuildServiceProvider()
            .GetRequiredService<ILogger<Program>>();

        var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
        levels = levels.Where(it => it != LogLevel.None).ToArray();
        var eventId = 1;
        Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {0} log message.", level));
        Console.Read();

        static bool Filter(string provider, string category, LogLevel level)
        {
            if (provider == typeof(ConsoleLoggerProvider).FullName)
            {
                return level >= LogLevel.Debug;
            }
            else if (provider == typeof(DebugLoggerProvider).FullName)
            {
                return level >= LogLevel.Warning;
            }
            else
            {
                return true;
            }
        }
    }
}
}
