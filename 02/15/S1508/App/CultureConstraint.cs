using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Globalization;

namespace App
{
    public class CultureConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            try
            {
                if (values.TryGetValue(routeKey, out object value))
                {
                    return !new CultureInfo(value.ToString()).EnglishName.StartsWith("Unknown Language");
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}