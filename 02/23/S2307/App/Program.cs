using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var options = new HealthCheckOptions
            {
                ResponseWriter = ReportAsync
            };

            var validConnString = @"Server=.;Database=TestDb;Uid=sa;Pwd=password";
            var invalidConnString = @"Server=.;Database=TestDb;Uid=sa;Pwd=passwordX";


            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .ConfigureServices(svcs => svcs
                        .AddDbContext<FooContext>(options => options.UseSqlServer(validConnString))
                        .AddDbContext<BarContext>(options => options.UseSqlServer(invalidConnString))
                        .AddHealthChecks()
                            .AddDbContextCheck<FooContext>()
                            .AddDbContextCheck<BarContext>())
                    .Configure(app => app.UseHealthChecks("/healthcheck", options)))
                .Build()
                .Run();

            static Task ReportAsync(HttpContext context, HealthReport report)
            {
                var builder = new StringBuilder();
                builder.AppendLine($"Status: {report.Status}");
                foreach (var name in report.Entries.Keys)
                {
                    builder.AppendLine($"    {name}: {report.Entries[name].Status}");
                }
                return context.Response.WriteAsync(builder.ToString());
            }
        }
    }
}