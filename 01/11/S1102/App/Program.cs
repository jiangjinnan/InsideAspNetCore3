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

        private sealed class StringContentMiddleware
        {
            private readonly RequestDelegate _next;
            private readonly string _contents;
            private readonly bool _forewardToNext;

            public StringContentMiddleware(RequestDelegate next, string contents, bool forewardToNext = true)
            {
                _next = next;
                _forewardToNext = forewardToNext;
                _contents = contents;
            }

            public async Task Invoke(HttpContext context)
            {
                await context.Response.WriteAsync(_contents);
                if (_forewardToNext)
                {
                    await _next(context);
                }
            }
        }
    }
}
