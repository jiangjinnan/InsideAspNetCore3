using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            var options = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All,
                ForwardLimit = 2
            };
            options.KnownNetworks.Add(new IPNetwork(IPAddress.Parse("192.168.0.0"), 28));

            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app
                        .UseForwardedHeaders(options)
                        .Run(ProcessAsync)))
                .Build()
                .Run();

            static async Task ProcessAsync(HttpContext context)
            {
                var request = context.Request;
                var response = context.Response;
                var headers = request.Headers;

                await response.WriteAsync($"Remote IP:{context.Connection.RemoteIpAddress}\n");
                await response.WriteAsync($"Host:{request.Host}\n");
                await response.WriteAsync($"Scheme:{request.Scheme}\n");

                await response.WriteAsync($"X-Original-For:{headers["X-Original-For"]}\n");
                await response.WriteAsync($"X-Original-Host:{headers["X-Original-Host"]}\n");
                await response.WriteAsync($"X-Original-Proto:{headers["X-Original-Proto"]}");
            }
        }
    }
}
