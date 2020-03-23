using Microsoft.AspNetCore.Hosting;
using System;

namespace App
{
    public class Foo : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder) => Console.WriteLine("Foo.Configure()");
    }
}
