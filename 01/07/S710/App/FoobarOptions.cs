using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class FoobarOptions : IEquatable<FoobarOptions>
    {
        public int Foo { get; set; }
        public int Bar { get; set; }

        public FoobarOptions() { }
        public FoobarOptions(int foo, int bar)
        {
            Foo = foo;
            Bar = bar;
        }

        public override string ToString() => $"Foo:{Foo}, Bar:{Bar}";
        public bool Equals(FoobarOptions other) => this.Foo == other?.Foo && this.Bar == other?.Bar;
    }

}
