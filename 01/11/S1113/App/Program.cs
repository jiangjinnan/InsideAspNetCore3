using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .UseSetting("Foobar:Foo", "Foo")
                .UseSetting("Foobar:Bar", "Bar")
                .UseSetting("Baz", "Baz")
                .UseStartup<Startup>())
            .Build()
            .Run();
        }
    }
}
