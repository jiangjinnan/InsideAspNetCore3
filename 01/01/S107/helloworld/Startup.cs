using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace helloworld
{
public class Startup
{
    public void ConfigureServices(IServiceCollection services) => services
        .AddRouting()
        .AddControllersWithViews();

    public void Configure(IApplicationBuilder app) => app
        .UseRouting()
        .UseEndpoints(endpoints => endpoints.MapControllers());
}
}