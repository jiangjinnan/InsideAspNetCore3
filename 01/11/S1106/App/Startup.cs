using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services) => services.AddSingleton<IBar, Bar>();
        public void Configure(IApplicationBuilder app, IFoo foo, IBar bar)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
        }
    }
}