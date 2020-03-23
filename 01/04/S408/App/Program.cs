using Microsoft.Extensions.DependencyInjection;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient<IQux, Qux>()
                .BuildServiceProvider()
                .GetServices<IQux>();
        }
    }
}
