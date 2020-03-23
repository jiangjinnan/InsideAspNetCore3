using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureServices(svcs => svcs.AddSingleton(new StringContentMiddleware("Hello World!")))
                .ConfigureWebHostDefaults(builder => builder.Configure(app => app.UseMiddleware<StringContentMiddleware>()))
            .Build()
            .Run();
        }

        private sealed class StringContentMiddleware : IMiddleware
        {
            private readonly string _contents;
            public StringContentMiddleware(string contents)=> _contents = contents;
            public Task InvokeAsync(HttpContext context, RequestDelegate next)=> context.Response.WriteAsync(_contents);
        }
    }
}
