using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_FOOBAR:FOO", "Foo");
            Environment.SetEnvironmentVariable("ASPNETCORE_FOOBAR:BAR", "Bar");
            Environment.SetEnvironmentVariable("ASPNETCORE_Baz", "Baz");

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
                .Build()
                .Run();
        }
    }
}
