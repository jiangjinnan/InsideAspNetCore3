using System;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = new Cat()
            .Register<IFoo, Foo>(Lifetime.Transient)
            .Register<IBar>(_ => new Bar(), Lifetime.Self)
            .Register<IBaz, Baz>(Lifetime.Root)
            .Register(Assembly.GetEntryAssembly());
            var cat1 = root.CreateChild();
            var cat2 = root.CreateChild();

            void GetServices<TService>(Cat cat)
            {
                cat.GetService<TService>();
                cat.GetService<TService>();
            }

            GetServices<IFoo>(cat1);
            GetServices<IBar>(cat1);
            GetServices<IBaz>(cat1);
            GetServices<IQux>(cat1);
            Console.WriteLine();
            GetServices<IFoo>(cat2);
            GetServices<IBar>(cat2);
            GetServices<IBaz>(cat2);
            GetServices<IQux>(cat2);

        }
    }
}
