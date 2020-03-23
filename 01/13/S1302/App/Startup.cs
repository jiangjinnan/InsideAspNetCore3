using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, DiagnosticListener listener)
        {
            listener.SubscribeWithAdapter(new DiagnosticCollector());
            app.Run(context =>
            {
                if (context.Request.Path == new PathString("/error"))
                {
                    throw new InvalidOperationException("Manually throw exception.");
                }
                return Task.CompletedTask;
            });
        }
    }
}