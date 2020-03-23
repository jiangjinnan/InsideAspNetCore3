using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public static partial class Extensions
    {
        public static T Get<T>(this IFeatureCollection features) => features.TryGetValue(typeof(T), out var value) ? (T)value : default(T);
        public static IFeatureCollection Set<T>(this IFeatureCollection features, T feature)
        {
            features[typeof(T)] = feature;
            return features;
        }

        public static WebHostBuilder UseHttpListenerServer(this WebHostBuilder builder, params string[] urls)
        {
            builder.HostBuilder.ConfigureServices(svcs => svcs.AddSingleton<IServer>(new HttpListenerServer(urls)));
            return builder;
        }

        public static WebHostBuilder Configure(this WebHostBuilder builder, Action<IApplicationBuilder> configure)
        {
            configure?.Invoke(builder.ApplicationBuilder);
            return builder;
        }

        public static IHostBuilder ConfigureWebHost(this IHostBuilder builder, Action<WebHostBuilder> configure)
        {
            var webHostBuilder = new WebHostBuilder(builder, new ApplicationBuilder());
            configure?.Invoke(webHostBuilder);
            builder.ConfigureServices(svcs => svcs.AddSingleton<IHostedService>(provider =>
            {
                var server = provider.GetRequiredService<IServer>();
                var handler = webHostBuilder.ApplicationBuilder.Build();
                return new WebHostedService(server, handler);
            }));
            return builder;
        }

        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}