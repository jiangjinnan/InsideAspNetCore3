using System;
using System.Diagnostics;
using System.IO;

namespace App
{
    class Program
    {
        static void Main()
        {
            var fileName = "trace.csv";
            File.AppendAllText(fileName, $"SourceName,EventType,EventId,Message,N/A,ProcessId, LogicalOperationStack, ThreadId, DateTime, Timestamp,{ Environment.NewLine}");

            using (var fileStream = new FileStream(fileName, FileMode.Append))
            {
                TraceOptions options = TraceOptions.Callstack | TraceOptions.DateTime |TraceOptions.LogicalOperationStack | TraceOptions.ProcessId |TraceOptions.ThreadId | TraceOptions.Timestamp;
                var listener = new DelimitedListTraceListener(fileStream){ TraceOutputOptions = options, Delimiter = "," };
                var source = new TraceSource("Foobar", SourceLevels.All);
                source.Listeners.Add(listener);
                var eventTypes = (TraceEventType[])Enum.GetValues(typeof(TraceEventType));
                for (int index = 0; index < eventTypes.Length; index++)
                {
                    var enventType = eventTypes[index];
                    var eventId = index + 1;
                    Trace.CorrelationManager.StartLogicalOperation($"Op{eventId}");
                    source.TraceEvent(enventType, eventId, $"This is a {enventType} message.");
                }
                source.Flush();
            }
        }
    }
}
