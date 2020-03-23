using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Diagnostics;

namespace App
{
    public class DiagnosticCollector
    {
        [DiagnosticName("Microsoft.AspNetCore.Hosting.BeginRequest")]
        public void OnRequestStart(HttpContext httpContext, long timestamp)
        {
            var request = httpContext.Request;
            Console.WriteLine($"\nRequest starting {request.Protocol} {request.Method} { request.Scheme}://{request.Host}{request.PathBase}{request.Path}");
            httpContext.Items["StartTimestamp"] = timestamp;
        }

        [DiagnosticName("Microsoft.AspNetCore.Hosting.EndRequest")]
        public void OnRequestEnd(HttpContext httpContext, long timestamp)
        {
            var startTimestamp = long.Parse(httpContext.Items["StartTimestamp"].ToString());
            var timestampToTicks = TimeSpan.TicksPerSecond / (double)Stopwatch.Frequency;
            var elapsed = new TimeSpan((long)(timestampToTicks * (timestamp - startTimestamp)));
            Console.WriteLine($"Request finished in {elapsed.TotalMilliseconds}ms { httpContext.Response.StatusCode}");
        }
        [DiagnosticName("Microsoft.AspNetCore.Hosting.UnhandledException")]
        public void OnException(HttpContext httpContext, long timestamp, Exception exception)
        {
            OnRequestEnd(httpContext, timestamp);
            Console.WriteLine($"{exception.Message}\nType:{exception.GetType()}\nStacktrace: { exception.StackTrace}");
        }
    }
}