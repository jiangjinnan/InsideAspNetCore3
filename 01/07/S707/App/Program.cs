using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Options;
using System;

namespace App
{
class Program
{
    public static void Main(string[] args)
    {
        var environment = new ConfigurationBuilder()
            .AddCommandLine(args)
            .Build()["env"];

        var services = new ServiceCollection();
        services
            .AddSingleton<IHostEnvironment>(new HostingEnvironment { EnvironmentName = environment })
            .AddOptions<DateTimeFormatOptions>().Configure<IHostEnvironment>(
        (options, env) => {
            if (env.IsDevelopment())
            {
                options.DatePattern = "dddd, MMMM d, yyyy";
                options.TimePattern = "M/d/yyyy";
            }
                
            else
            {
                options.DatePattern = "M/d/yyyy";
                options.TimePattern = "h:mm tt";
            }
        });

        var options = services
            .BuildServiceProvider()
            .GetRequiredService<IOptions<DateTimeFormatOptions>>().Value;
        Console.WriteLine(options);
    }
}
}
