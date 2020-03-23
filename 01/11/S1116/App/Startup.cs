using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace App
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services) => services.Configure<FoobarOptions>(_configuration);

        public void Configure(IApplicationBuilder app, IOptions<FoobarOptions> optionsAccessor)
        {
            var options = optionsAccessor.Value;
            var json = JsonConvert.SerializeObject(options, Formatting.Indented);
            app.Run(async context =>
            {
                context.Response.ContentType = "text/html";
                await context.Response.WriteAsync($"<pre>{json}</pre>");
            });
        }
    }

}