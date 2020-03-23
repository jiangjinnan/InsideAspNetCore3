using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class FakeMetricsDeliverer : IMetricsDeliverer
    {
        public Task DeliverAsync(PerformanceMetrics counter)
        {
            Console.WriteLine($"[{DateTimeOffset.UtcNow}]{counter}");
            return Task.CompletedTask;
        }
    }
}
