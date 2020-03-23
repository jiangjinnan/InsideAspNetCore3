using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main()
        {
            new HostBuilder()
                .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
                .Build()
                .Run();
        }
    }
}
