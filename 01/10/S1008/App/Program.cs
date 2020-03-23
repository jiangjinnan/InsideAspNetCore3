using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace App
{
    class Program
    {
        static void Main()
        {
            new HostBuilder()
                .ConfigureServices(svcs => svcs.AddHostedService<FakeHostedService>())
                .UseServiceProviderFactory(new CatServiceProviderFactory())
                .ConfigureContainer<CatBuilder>(builder => builder.Register(Assembly.GetEntryAssembly()))
                .Build()
                .Run();
        }
    }

}