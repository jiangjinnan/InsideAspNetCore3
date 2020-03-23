using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureAppConfiguration(config => config
                    .AddInMemoryCollection(new Dictionary<string, string>
                    {
                        ["Foobar:Foo"] = "Foo",
                        ["Foobar:Bar"] = "Bar",
                        ["Baz"] = "Baz"
                    }))
                .UseStartup<Startup>())
            .Build()
            .Run();
        }
    }
}
