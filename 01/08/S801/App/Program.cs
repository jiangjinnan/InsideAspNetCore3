using System;
using System.Diagnostics;

namespace App
{
    class Program
    {
        static void Main()
        {
            var source = new TraceSource("Foobar", SourceLevels.All);
            var eventTypes = (TraceEventType[])Enum.GetValues(typeof(TraceEventType));
            var eventId = 1;
            Array.ForEach(eventTypes, it => source.TraceEvent(it, eventId++, $"This is a {it} message."));
            Console.Read();
        }
    }
}
