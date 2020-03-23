using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public class FakeMetricsCollector : IProcessorMetricsCollector, IMemoryMetricsCollector, INetworkMetricsCollector
    {
        long INetworkMetricsCollector.GetThroughput() => PerformanceMetrics.Create().Network;
        int IProcessorMetricsCollector.GetUsage() => PerformanceMetrics.Create().Processor;
        long IMemoryMetricsCollector.GetUsage() => PerformanceMetrics.Create().Memory;
    }

}
