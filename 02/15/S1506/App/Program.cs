using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        private static Dictionary<string, string> _cities = new Dictionary<string, string>
        {
            ["010"] = "北京",
            ["028"] = "成都",
            ["0512"] = "苏州"
        };

        public static void Main()
        {
            var template = "weather/{city}/{*date}";
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs.AddRouting())
                    .Configure(app => app
                        .UseRouting()
                        .UseEndpoints(routes => routes.MapGet(template, WeatherForecast))))
                .Build()
                .Run();
        }


        public static async Task WeatherForecast(HttpContext context)
        {
            var values = context.GetRouteData().Values;
            var city = values["city"].ToString();
            city = _cities[city];
            var date = DateTime.ParseExact(values["date"].ToString(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            var report = new WeatherReport(city, date);
            await RendWeatherAsync(context, report);
        }

        private static async Task RendWeatherAsync(HttpContext context, WeatherReport report)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            await context.Response.WriteAsync("<html><head><title>Weather</title></head><body>");
            await context.Response.WriteAsync($"<h3>{report.City}</h3>");
            foreach (var it in report.WeatherInfos)
            {
                await context.Response.WriteAsync($"{it.Key.ToString("yyyy-MM-dd")}:");
                await context.Response.WriteAsync($"{it.Value.Condition}({ it.Value.LowTemperature}℃ ~ { it.Value.HighTemperature}℃)<br/><br/> ");
            }
            await context.Response.WriteAsync("</body></html>");
        }
    }
}