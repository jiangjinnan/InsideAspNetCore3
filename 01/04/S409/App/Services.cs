using System;

namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }
    public interface IQux { }

    public class Foo : IFoo { }
    public class Bar : IBar { }
    public class Baz : IBaz { }
    public class Qux : IQux
    {
        public Qux(IFoo foo, IBar bar) { }
        public Qux(IBar bar, IBaz baz) { }
    }
}
