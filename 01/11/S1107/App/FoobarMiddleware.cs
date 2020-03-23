using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App
{
    public class FoobarMiddleware : IMiddleware
    {
        public FoobarMiddleware(IFoo foo, IBar bar)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Debug.Assert(next != null);
            return Task.CompletedTask;
        }
    }
}