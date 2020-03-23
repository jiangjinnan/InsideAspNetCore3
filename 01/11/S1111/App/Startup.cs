using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IFoo foo, IBar bar, IBaz baz)
        {
            app.Run(async context =>
            {
                var response = context.Response;
                response.ContentType = "text/html";
                await response.WriteAsync($"foo: {foo}<br/>");
                await response.WriteAsync($"bar: {bar}<br/>");
                await response.WriteAsync($"baz: {baz}<br/>");
            });
        }
        public void ConfigureContainer(CatBuilder container)=> container.Register(Assembly.GetEntryAssembly());
    }

}