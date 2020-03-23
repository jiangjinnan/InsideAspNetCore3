using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;

namespace App
{
    public sealed class FoobarSource : EventSource
    {
        public static readonly FoobarSource Instance = new FoobarSource();
        private FoobarSource() : base(EventSourceSettings.EtwSelfDescribingEventFormat) { }

        [Event(1)]
        public void Test(Payload payload) => WriteEvent(1, payload);
    }

}
