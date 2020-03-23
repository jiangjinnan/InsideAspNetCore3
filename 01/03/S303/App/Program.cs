using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new Cat()
                .Register<Base, Foo>(Lifetime.Transient)
                .Register<Base, Bar>(Lifetime.Transient)
                .Register<Base, Baz>(Lifetime.Transient)
                .GetServices<Base>();
            Debug.Assert(services.OfType<Foo>().Any());
            Debug.Assert(services.OfType<Bar>().Any());
            Debug.Assert(services.OfType<Baz>().Any());
        }
    }
}
