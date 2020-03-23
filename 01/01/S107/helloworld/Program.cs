using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace helloworld
{
class Program
{
    static void Main()
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(webHostBuilder => webHostBuilder.UseStartup<Startup>())
            .Build()
            .Run();
    }
}
}
