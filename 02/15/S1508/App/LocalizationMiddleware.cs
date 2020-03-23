using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Globalization;
using System.Threading.Tasks;

namespace App
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _routeKey;

        public LocalizationMiddleware(RequestDelegate next, string routeKey)
        {
            _next = next;
            _routeKey = routeKey;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var currentUICulture = CultureInfo.CurrentUICulture;
            try
            {
                if (context.GetRouteData().Values.TryGetValue(_routeKey, out var culture))
                {
                    CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new CultureInfo(culture.ToString());
                }
                await _next(context);
            }
            finally
            {
                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentUICulture;
            }
        }
    }
}