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
                    .ConfigureServices(svcs => svcs.AddHostFiltering(options =>
                    {
                        options.AllowedHosts.Add("www.foo.com");
                        options.AllowedHosts.Add("www.bar.com");
                    }))
                    .Configure(app => app
                        .UseHostFiltering()
                        .Run(contenxt => contenxt.Response.WriteAsync($"{contenxt.Request.Host} is valid!"))))
                .Build()
                .Run();
        }
    }
}
