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
            var listener = new FoobarEventListener();
            listener.EventSourceCreated += (sender, args) =>
            {
                if (args.EventSource.Name == "Microsoft-Extensions-Logging")
                {
                    listener.EnableEvents(args.EventSource, EventLevel.LogAlways);
                }
            };
            listener.EventWritten += (sender, args) =>
            {
                if (args.EventName == "FormattedMessage")
                {
                    var payload = args.Payload;
                    var payloadNames = args.PayloadNames;
                    var indexOfLevel = payloadNames.IndexOf("Level");
                    var indexOfCategory = args.PayloadNames.IndexOf("LoggerName");
                    var indexOfEventId = args.PayloadNames.IndexOf("EventId");
                    var indexOfMessage = args.PayloadNames.IndexOf("FormattedMessage");
                    Console.WriteLine($"{(LogLevel)payload[indexOfLevel],-11}: { payload[indexOfCategory]}[{ payload[indexOfEventId]}]");
                    Console.WriteLine($"{"",-13}{payload[indexOfMessage]}");
                }
            };

            var logger = new ServiceCollection()
                .AddLogging(builder => builder
                    .AddTraceSource(new SourceSwitch("default", "All"), new DefaultTraceListener { LogFileName = "trace.log" })
                    .AddEventSourceLogger())
                .BuildServiceProvider()
                .GetRequiredService<ILogger<Program>>();

            var levels = (LogLevel[])Enum.GetValues(typeof(LogLevel));
            levels = levels.Where(it => it != LogLevel.None).ToArray();
            var eventId = 1;
            Array.ForEach(levels, level => logger.Log(level, eventId++, "This is a/an {level} log message.", level));
        }
        public class FoobarEventListener : EventListener { }
    }
}
