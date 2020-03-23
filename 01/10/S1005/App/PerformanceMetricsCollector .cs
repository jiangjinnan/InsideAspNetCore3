using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public sealed class PerformanceMetricsCollector : IHostedService
    {
        private readonly IProcessorMetricsCollector _processorMetricsCollector;
        private readonly IMemoryMetricsCollector _memoryMetricsCollector;
        private readonly INetworkMetricsCollector _networkMetricsCollector;
        private readonly IMetricsDeliverer _metricsDeliverer;
        private readonly TimeSpan _captureInterval;
        private IDisposable _scheduler;

        public PerformanceMetricsCollector(
            IProcessorMetricsCollector processorMetricsCollector,
            IMemoryMetricsCollector memoryMetricsCollector,
            INetworkMetricsCollector networkMetricsCollector,
            IMetricsDeliverer MetricsDeliverer,
            IOptions<MetricsCollectionOptions> optionsAccessor)
        {
            _processorMetricsCollector = processorMetricsCollector;
            _memoryMetricsCollector = memoryMetricsCollector;
            _networkMetricsCollector = networkMetricsCollector;
            _metricsDeliverer = MetricsDeliverer;
            _captureInterval = optionsAccessor.Value.CaptureInterval;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _scheduler = new Timer(Callback, null, TimeSpan.FromSeconds(5), _captureInterval);
            return Task.CompletedTask;

            async void Callback(object state)
            {
                var counter = new PerformanceMetrics
                {
                    Processor = _processorMetricsCollector.GetUsage(),
                    Memory = _memoryMetricsCollector.GetUsage(),
                    Network = _networkMetricsCollector.GetThroughput()
                };
                await _metricsDeliverer.DeliverAsync(counter);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _scheduler?.Dispose();
            return Task.CompletedTask;
        }
    }
}
