using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace App
{
    public class LoggingEventListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "Microsoft-Extensions-Logging")
            {
                EnableEvents(eventSource, EventLevel.LogAlways);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            Console.WriteLine($"Event: {eventData.EventName}");
            for (int index = 0; index < eventData.Payload.Count; index++)
            {
                var element = eventData.Payload[index];
                if (element is object[] || element is IDictionary<string, object>)
                {
                    Console.WriteLine($"{eventData.PayloadNames[index],-16}: { JsonConvert.SerializeObject(element)}");
                    continue;
                }
                Console.WriteLine($"{eventData.PayloadNames[index],-16}: { eventData.Payload[index]}");
            }
            Console.WriteLine();
        }
    }
}