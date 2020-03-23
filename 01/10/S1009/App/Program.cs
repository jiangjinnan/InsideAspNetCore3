using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            new HostBuilder()
                .ConfigureHostConfiguration(builder => builder.AddCommandLine(args))
                .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
                .Build()
                .Run();
        }
    }

}
