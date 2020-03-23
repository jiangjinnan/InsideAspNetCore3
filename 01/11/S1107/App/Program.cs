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
            .ConfigureServices(svcs => svcs
                .AddSingleton<FoobarMiddleware>()
                .AddSingleton<IFoo, Foo>()
                .AddSingleton<IBar, Bar>())
            .Configure(app => app.UseMiddleware<FoobarMiddleware>()))
        .Build()
        .Run();
    }
}
}
