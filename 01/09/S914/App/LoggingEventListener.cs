using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;

namespace App
{
    public class LoggingEventListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "System.Threading.Tasks.TplEventSource")
            {
                EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)0x80);
            }

            if (eventSource.Name == "Microsoft-Extensions-Logging")
            {
                EnableEvents(eventSource, EventLevel.LogAlways, (EventKeywords)8);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            int index;
            var payload = (index = eventData.PayloadNames.IndexOf("ArgumentsJson")) == -1
                ? null
                : eventData.Payload[index];
            var relatedActivityId = eventData.RelatedActivityId == default(Guid)
                ? ""
                : eventData.RelatedActivityId.ToString();

            File.AppendAllLines("log.csv", new string[] { $"{eventData.EventName}, {payload}, {eventData.ActivityId}, {relatedActivityId}" });
        }
    }
}