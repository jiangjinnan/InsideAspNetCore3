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
        public Qux(IFoo foo) => Console.WriteLine("Selected constructor: Qux(IFoo)");
        public Qux(IFoo foo, IBar bar) => Console.WriteLine("Selected constructor: Qux(IFoo, IBar)");
        public Qux(IFoo foo, IBar bar, IBaz baz) => Console.WriteLine("Selected constructor: Qux(IFoo, IBar, IBaz)");
    }

}
