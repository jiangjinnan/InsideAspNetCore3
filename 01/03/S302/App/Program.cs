using System;
using System.Diagnostics;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var cat = new Cat()
                .Register<IFoo, Foo>(Lifetime.Transient)
                .Register<IBar, Bar>(Lifetime.Transient)
                .Register(typeof(IFoobar<,>), typeof(Foobar<,>), Lifetime.Transient);

            var foobar = (Foobar<IFoo, IBar>)cat.GetService<IFoobar<IFoo, IBar>>();
            Debug.Assert(foobar.Foo is Foo);
            Debug.Assert(foobar.Bar is Bar);
        }
    }
}
