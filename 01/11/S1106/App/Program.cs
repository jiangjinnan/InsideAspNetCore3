using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(builder => builder
                .UseStartup<Startup>()
                .ConfigureServices(svcs => svcs.AddSingleton<IFoo, Foo>()))
            .Build()
            .Run();
        }
    }
}
