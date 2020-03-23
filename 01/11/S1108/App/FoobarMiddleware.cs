using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App
{
    public class FoobarMiddleware
    {
        private readonly RequestDelegate _next;
        public FoobarMiddleware(RequestDelegate next) => _next = next;
        public Task InvokeAsync(HttpContext context, IFoo foo, IBar bar)
        {
            Debug.Assert(context != null);
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
            return _next(context);
        }
    }
}