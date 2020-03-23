using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static void Main()
        {
            DiagnosticListener.AllListeners.Subscribe(listener =>
            {
                if (listener.Name == "Web")
                {
                    listener.SubscribeWithAdapter(new DiagnosticCollector());
                }
            });


            var source = new DiagnosticListener("Web");
            var stopwatch = Stopwatch.StartNew();
            if (source.IsEnabled("ReceiveRequest"))
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "https://www.artech.top");
                source.Write("ReceiveRequest", new
                {
                    Request = request,
                    Timestamp = Stopwatch.GetTimestamp()
                });
            }
            Task.Delay(100).Wait();
            if (source.IsEnabled("SendReply"))
            {
                var response = new HttpResponseMessage(HttpStatusCode.OK);
                source.Write("SendReply", new
                {
                    Response = response,
                    Elaped = stopwatch.Elapsed
                });
            }
        }
    }

}
