using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace App
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            Console.WriteLine($"ApplicationName: {environment.ApplicationName}");
            Console.WriteLine($"EnvironmentName: {environment.EnvironmentName}");
            Console.WriteLine($"ContentRootPath: {environment.ContentRootPath}");
            Console.WriteLine($"WebRootPath: {environment.WebRootPath}");
        }
        public void Configure(IApplicationBuilder app) { }
    }
}


