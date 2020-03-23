using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public interface IFoo { }
    public interface IBar { }
    public interface IBaz { }

    [MapTo(typeof(IFoo), Lifetime.Root)]
    public class Foo : IFoo { }

    [MapTo(typeof(IBar), Lifetime.Root)]
    public class Bar : IBar { }

    [MapTo(typeof(IBaz), Lifetime.Root)]
    public class Baz : IBaz { }

}
