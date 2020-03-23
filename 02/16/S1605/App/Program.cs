using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.Configure(app => app
                   .UseStatusCodePages("text/plain", "Error occurred ({0})")
                   .Run(context => Task.Run(() => context.Response.StatusCode = 500))))
            .Build()
            .Run();
        }
    }
}