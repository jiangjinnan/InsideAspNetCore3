using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .UseStartup<Startup>())
                .UseServiceProviderFactory(new CatServiceProviderFactory())
                .Build()
                .Run();
        }
    }
}