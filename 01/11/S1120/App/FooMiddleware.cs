using Microsoft.AspNetCore.Http;

namespace App
{
    public class FooMiddleware : StringContentMiddleware
    {
        public FooMiddleware(RequestDelegate next) : base(next, "Foo=>", "Foo") { }
    }
}