using System.Collections.Generic;
using System.Diagnostics.Tracing;

namespace App
{
    [EventData]
    public class Payload
    {
        public Foobar Foobar { get; set; }
        public IEnumerable<Foobar> Collection { get; set; }
        public IDictionary<int, Foobar> Dictionary { get; set; }
    }
}