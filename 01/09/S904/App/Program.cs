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
            var logger = new ServiceCollection().AddLogging(builder => builder
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole())
                .BuildServiceProvider()
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger<Program>();

            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(it => it != LogLevel.None).ToArray();
            var eventId = 1;
            Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {level} log message.", level));
            Console.Read();
        }
    }
}
