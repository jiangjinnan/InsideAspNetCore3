using Microsoft.AspNetCore.Http;

namespace App
{
    public class BarMiddleware : StringContentMiddleware
    {
        public BarMiddleware(RequestDelegate next) : base(next, "Bar=>", "Bar=>") { }
    }
}