using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace App
{
    public class DatabaseSourceListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "Artech-Data-SqlClient")
            {
                EnableEvents(eventSource, EventLevel.LogAlways);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            Console.WriteLine($"EventId: {eventData.EventId}");
            Console.WriteLine($"EventName: {eventData.EventName}");
            Console.WriteLine($"Channel: {eventData.Channel}");
            Console.WriteLine($"Keywords: {eventData.Keywords}");
            Console.WriteLine($"Level: {eventData.Level}");
            Console.WriteLine($"Message: {eventData.Message}");
            Console.WriteLine($"Opcode: {eventData.Opcode}");
            Console.WriteLine($"Tags: {eventData.Tags}");
            Console.WriteLine($"Task: {eventData.Task}");
            Console.WriteLine($"Version: {eventData.Version}");
            Console.WriteLine($"Payload");
            var index = 0;
            foreach (var payloadName in eventData.PayloadNames)
            {
                Console.WriteLine($"\t{payloadName}:{eventData.Payload[index++]}");
            }
        }
    }

}
