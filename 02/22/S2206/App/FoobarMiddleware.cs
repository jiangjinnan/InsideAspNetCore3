using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace App
{
    public class FoobarMiddleware
    {
        public FoobarMiddleware(RequestDelegate _) { }
        public Task InvokeAsync(HttpContext context, IStringLocalizer<FoobarMiddleware> localizer)
        {
            context.Response.ContentType = "text/plain;charset=utf-8";
            return context.Response.WriteAsync(localizer.GetString("Greeting"));
        }
    }
}