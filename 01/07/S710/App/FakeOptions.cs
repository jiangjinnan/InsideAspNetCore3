using System.Collections.Generic;

namespace App
{
    public class FakeOptions
    {
        public FoobarOptions Foobar { get; set; }
        public FoobarOptions[] Array { get; set; }
        public IList<FoobarOptions> List { get; set; }
        public IDictionary<string, FoobarOptions> Dictionary { get; set; }    }

}