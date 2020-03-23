using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.IO;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureLogging(options => options.ClearProviders())
                .UseStartup<Startup>()
                .UseSetting("environment", "Staging")
                .UseSetting("contentRoot", Path.Combine(Directory.GetCurrentDirectory(), "contents"))
                .UseSetting("webroot", Path.Combine(Directory.GetCurrentDirectory(), "contents/web"))
                .UseSetting("ApplicationName", "MyApp"))
            .Build()
            .Run();
        }
    }
}
