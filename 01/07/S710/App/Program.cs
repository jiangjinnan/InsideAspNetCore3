using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Globalization;

namespace App
{
    class Program
    {
        static void Main()
        {
            var foobar1 = new FoobarOptions(1, 1);
            var foobar2 = new FoobarOptions(2, 2);
            var foobar3 = new FoobarOptions(3, 3);

            var options = new ServiceCollection()
                .AddOptions()
                .Configure<FakeOptions>("fakeoptions.json")
                .BuildServiceProvider()
                .GetRequiredService<IOptions<FakeOptions>>()
                .Value;

            Debug.Assert(options.Foobar.Equals(foobar1));

            Debug.Assert(options.Array[0].Equals(foobar1));
            Debug.Assert(options.Array[1].Equals(foobar2));
            Debug.Assert(options.Array[2].Equals(foobar3));

            Debug.Assert(options.List[0].Equals(foobar1));
            Debug.Assert(options.List[1].Equals(foobar2));
            Debug.Assert(options.List[2].Equals(foobar3));

            Debug.Assert(options.Dictionary["1"].Equals(foobar1));
            Debug.Assert(options.Dictionary["2"].Equals(foobar2));
            Debug.Assert(options.Dictionary["3"].Equals(foobar3));
        }
    }

}
