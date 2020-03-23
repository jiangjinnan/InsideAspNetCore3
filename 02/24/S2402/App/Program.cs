using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder
                   .Configure(app => app
                       .UseHttpMethodOverride()
                       .Run(contenxt => contenxt.Response.WriteAsync($"HTTP Method: {contenxt.Request.Method}"))))
                .Build()
                .Run();
        }
    }
}
