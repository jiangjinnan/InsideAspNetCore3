using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Text;

namespace App
{
    public sealed class FoobarListener : EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            if (eventSource.Name == "System.Threading.Tasks.TplEventSource")
            {
                EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)0x80);
            }

            if (eventSource.Name == "Foobar")
            {
                EnableEvents(eventSource, EventLevel.LogAlways);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            if (eventData.EventSource.Name == "Foobar")
            {
                var timestamp = eventData.PayloadNames[0] == "timestamp"
                    ? eventData.Payload[0]
                    : "";
                var elapsed = eventData.PayloadNames[0] == "elapsed"
                    ? eventData.Payload[0]
                    : "";
                var relatedActivityId = eventData.RelatedActivityId == default
                    ? ""
                    : eventData.RelatedActivityId.ToString();
                var line = $"{eventData.EventName},{timestamp},{elapsed},{ eventData.ActivityId},{ relatedActivityId}";
                File.AppendAllLines("log.csv", new string[] { line });
            }
        }
    }

}
