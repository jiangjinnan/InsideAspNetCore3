using System;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var root = new Cat()
                .Register<IFoo, Foo>(Lifetime.Transient)
                .Register<IBar>(_ => new Bar(), Lifetime.Self)
                .Register<IBaz, Baz>(Lifetime.Root)
                .Register(Assembly.GetEntryAssembly()))
            {
                using (var cat = root.CreateChild())
                {
                    cat.GetService<IFoo>();
                    cat.GetService<IBar>();
                    cat.GetService<IBaz>();
                    cat.GetService<IQux>();
                    Console.WriteLine("Child cat is disposed.");
                }
                Console.WriteLine("Root cat is disposed.");
            }
        }
    }
}
