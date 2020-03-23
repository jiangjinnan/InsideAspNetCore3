using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class FakeMetricsDeliverer : IMetricsDeliverer
    {
        private readonly TransportType _transport;
        private readonly Endpoint _deliverTo;

        public FakeMetricsDeliverer(IOptions<MetricsCollectionOptions> optionsAccessor)
        {
            var options = optionsAccessor.Value;
            _transport = options.Transport;
            _deliverTo = options.DeliverTo;
        }

        public Task DeliverAsync(PerformanceMetrics counter)
        {
            Console.WriteLine($"[{DateTimeOffset.Now}]Deliver performance counter {counter} to { _deliverTo} via { _transport}");
            return Task.CompletedTask;
        }
    }

}
