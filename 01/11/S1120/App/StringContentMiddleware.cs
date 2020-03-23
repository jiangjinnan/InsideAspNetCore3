using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace App
{
    public abstract class StringContentMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _preContents;
        private readonly string _postContents;

        public StringContentMiddleware(RequestDelegate next, string preContents, string postContents)
        {
            _next = next;
            _preContents = preContents;
            _postContents = postContents;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await context.Response.WriteAsync(_preContents);
            await _next(context);
            await context.Response.WriteAsync(_postContents);
        }
    }

}