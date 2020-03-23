using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Net.Http;

namespace App
{
    public sealed class DiagnosticCollector
    {
        [DiagnosticName("ReceiveRequest")]
        public void OnReceiveRequest(HttpRequestMessage request, long timestamp)
        => Console.WriteLine($"Receive request. Url: {request.RequestUri}; Timstamp:{timestamp}");

        [DiagnosticName("SendReply")]
        public void OnSendReply(HttpResponseMessage response, TimeSpan elaped)
        => Console.WriteLine($"Send reply. Status code: {response.StatusCode}; Elaped: {elaped}");
    }

}