using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            var root = new ServiceCollection()
                .AddSingleton<IFoo, Foo>()
                .AddScoped<IBar, Bar>()
                .BuildServiceProvider(true);
            var child = root.CreateScope().ServiceProvider;

            void ResolveService<T>(IServiceProvider provider)
            {
                var isRootContainer = root == provider ? "Yes" : "No";
                try
                {
                    provider.GetService<T>();
                    Console.WriteLine($"Status: Success; Service Type: { typeof(T).Name}; Root: { isRootContainer}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Status: Fail; Service Type: { typeof(T).Name}; Root: { isRootContainer}");
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            ResolveService<IFoo>(root);
            ResolveService<IBar>(root);
            ResolveService<IFoo>(child);
            ResolveService<IBar>(child);
        }
    }

    public interface IFoo { }
    public interface IBar { }
    public class Foo : IFoo
    {
        public IBar Bar { get; }
        public Foo(IBar bar) => Bar = bar;
    }
    public class Bar : IBar { }


}
