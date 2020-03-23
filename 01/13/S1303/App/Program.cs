using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.Tracing;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        private sealed class DiagnosticCollector : EventListener { }
        static void Main()
        {
            var listener = new DiagnosticCollector();
            listener.EventSourceCreated += (sender, args) =>
            {
                if (args.EventSource.Name == "Microsoft.AspNetCore.Hosting")
                {
                    listener.EnableEvents(args.EventSource, EventLevel.LogAlways);
                }
            };
            listener.EventWritten += (sender, args) =>
            {
                Console.WriteLine(args.EventName);
                for (int index = 0; index < args.PayloadNames.Count; index++)
                {
                    Console.WriteLine($"\t{args.PayloadNames[index]} = {args.Payload[index]}");
                }
            };

            Host.CreateDefaultBuilder()
                .ConfigureLogging(builder => builder.ClearProviders())
                .ConfigureWebHostDefaults(builder => builder
                    .Configure(app => app.Run(context =>
                    {
                        if (context.Request.Path == new PathString("/error"))
                        {
                            throw new InvalidOperationException("Manually throw exception.");
                        }
                        return Task.CompletedTask;
                    })))
                .Build()
                .Run();
        }
    }
}
