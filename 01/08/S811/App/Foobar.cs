using System.Diagnostics.Tracing;

namespace App
{
    [EventData]
    public class Foobar
    {
        public int Foo { get; set; }
        public int Bar { get; set; }

        public Foobar(int foo, int bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }
}