using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var provider = new ServiceCollection()
                .AddTransient<IFoo, Foo>()
                .AddTransient<IBar, Bar>()
                .AddTransient(typeof(IFoobar<,>), typeof(Foobar<,>))
                .BuildServiceProvider();

            var foobar = (Foobar<IFoo, IBar>)provider.GetService<IFoobar<IFoo, IBar>>();
            Debug.Assert(foobar.Foo is Foo);
            Debug.Assert(foobar.Bar is Bar);
        }
    }
}
