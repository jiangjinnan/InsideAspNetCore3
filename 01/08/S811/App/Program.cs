using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;

namespace App
{
    class Program
    {
        static void Main()
        {
            var listener = new FoobarListener();
            listener.EnableEvents(FoobarSource.Instance, EventLevel.LogAlways);
            listener.EventWritten += (sender, args) => {
                string ToString(object foobar)
                {
                    var dictionary = (IDictionary<string, object>)foobar;
                    return $"(Foo={dictionary["Foo"]}, Bar={dictionary["Bar"]})";
                }
                var eventData = (IDictionary<string, object>)args.Payload.Single();
                Console.WriteLine($"Foobar: {ToString(eventData["Foobar"])}");

                var array = (object[])eventData["Collection"];
                Console.WriteLine("Collection:");
                for (int index = 0; index < array.Length; index++)
                {
                    Console.WriteLine($"\t[{index}]: {ToString(array[index])}");
                }

                Console.WriteLine("Dictionary:");
                array = (object[])eventData["Dictionary"];
                foreach (IDictionary<string, object> eventPayload in array)
                {
                    var key = eventPayload["Key"];
                    Console.WriteLine($"\tKey: {key}: Value: {ToString(eventPayload["Value"])}");
                }
            };

            var payload = new Payload
            {
                Foobar = new Foobar(1, 2),
                Collection = new Foobar[] { new Foobar(11, 12), new Foobar(21, 22),
                new Foobar(31, 32) },
                Dictionary = new Dictionary<int, Foobar>
                {
                    [1] = new Foobar(11, 12),
                    [2] = new Foobar(21, 22),
                    [3] = new Foobar(31, 32)
                }
            };

            FoobarSource.Instance.Test(payload);
        }
    }

}
