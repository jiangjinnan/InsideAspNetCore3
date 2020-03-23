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
                .UseSetting("urls", "http://0.0.0.0:8888;http://0.0.0.0:9999")
                .UseStartup<Startup>())
            .Build()
            .Run();
        }

    }
}
