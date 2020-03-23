using Microsoft.Extensions.Hosting;
using System;

namespace App
{
class Program
{
    static void Main()
    {
        Host.CreateDefaultBuilder()
            .ConfigureWebHost(builder => builder
                .UseHttpListenerServer()
                .Configure(app => app
                    .Use(FooMiddleware)
                    .Use(BarMiddleware)
                    .Use(BazMiddleware)))
            .Build()
            .Run();
    }

    public static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

    public static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
    {
        await context.Response.WriteAsync("Bar=>");
        await next(context);
    };

    public static RequestDelegate BazMiddleware(RequestDelegate next)
    => context => context.Response.WriteAsync("Baz");
}
}
