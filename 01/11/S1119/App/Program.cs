using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using App;

[assembly: HostingStartup(typeof(Foo))]

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build();

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .ConfigureLogging(options => options.ClearProviders())
                .UseSetting("hostingStartupAssemblies", config["hostingStartupAssemblies"])
                .UseSetting("preventHostingStartup", config["preventHostingStartup"])
                .Configure(app => app.Run(context => Task.CompletedTask)))
            .Build()
            .Run();
        }
    }
}
