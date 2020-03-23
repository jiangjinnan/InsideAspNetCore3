using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IBaz, Baz>()
                .AddTransient<IQux, Qux>()
                .BuildServiceProvider()
                .GetServices<IQux>();
        }
    }
}
