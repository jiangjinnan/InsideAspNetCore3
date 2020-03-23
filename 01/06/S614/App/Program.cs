using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var mapping = new Dictionary<string, string>
                {
                    ["-a"] = "architecture",
                    ["-arch"] = "architecture"
                };
                var configuration = new ConfigurationBuilder()
                    .AddCommandLine(args, mapping)
                    .Build();
                Console.WriteLine($"Architecture: {configuration["architecture"]}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}