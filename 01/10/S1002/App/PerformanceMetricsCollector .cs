using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace App
{
    public sealed class PerformanceMetricsCollector : IHostedService
    {
        private readonly IProcessorMetricsCollector _processorMetricsCollector;
        private readonly IMemoryMetricsCollector _memoryMetricsCollector;
        private readonly INetworkMetricsCollector _networkMetricsCollector;
        private readonly IMetricsDeliverer _MetricsDeliverer;
        private IDisposable _scheduler;

        public PerformanceMetricsCollector(
            IProcessorMetricsCollector processorMetricsCollector,
            IMemoryMetricsCollector memoryMetricsCollector,
            INetworkMetricsCollector networkMetricsCollector,
            IMetricsDeliverer MetricsDeliverer)
        {
            _processorMetricsCollector = processorMetricsCollector;
            _memoryMetricsCollector = memoryMetricsCollector;
            _networkMetricsCollector = networkMetricsCollector;
            _MetricsDeliverer = MetricsDeliverer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
            return Task.CompletedTask;

            async void Callback(object state)
            {
                var counter = new PerformanceMetrics
                {
                    Processor = _processorMetricsCollector.GetUsage(),
                    Memory = _memoryMetricsCollector.GetUsage(),
                    Network = _networkMetricsCollector.GetThroughput()
                };
                await _MetricsDeliverer.DeliverAsync(counter);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }
}
