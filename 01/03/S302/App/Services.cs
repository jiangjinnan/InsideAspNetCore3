using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IGux { }
    public interface IFoobar<T1, T2> { }
    public class Base : IDisposable
    {
        public Base()
            => Console.WriteLine($"Instance of {GetType().Name} is created.");
        public void Dispose()
            => Console.WriteLine($"Instance of {GetType().Name} is disposed.");
    }

    public class Foo : Base, IFoo { }
    public class Bar : Base, IBar { }
    public class Baz : Base, IBaz { }
    [MapTo(typeof(IGux), Lifetime.Root)]
    public class Gux : Base, IGux { }
    public class Foobar<T1, T2> : IFoobar<T1, T2>
    {
        public IFoo Foo { get; }
        public IBar Bar { get; }
        public Foobar(IFoo foo, IBar bar)
        {
            Foo = foo;
            Bar = bar;
        }
    }

}
