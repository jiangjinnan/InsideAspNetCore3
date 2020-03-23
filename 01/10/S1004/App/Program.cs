using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var collector = new FakeMetricsCollector();
            new HostBuilder()
                .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
                .ConfigureAppConfiguration((context, builder) => builder
                    .AddJsonFile(path: "appsettings.json", optional: false)
                    .AddJsonFile(
                        path: $"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                        optional: true))
                .ConfigureServices((context, svcs) => svcs
                    .AddSingleton<IProcessorMetricsCollector>(collector)
                    .AddSingleton<IMemoryMetricsCollector>(collector)
                    .AddSingleton<INetworkMetricsCollector>(collector)
                    .AddSingleton<IMetricsDeliverer, FakeMetricsDeliverer>()
                    .AddSingleton<IHostedService, PerformanceMetricsCollector>()

                    .AddOptions()
                    .Configure<MetricsCollectionOptions>(context.Configuration.GetSection("MetricsCollection")))
                .Build()
                .Run();
        }
    }

}
