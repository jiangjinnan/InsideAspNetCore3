using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public sealed class FakeHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _lifetime;
        private IDisposable _tokenSource;

        public FakeHostedService(IHostApplicationLifetime lifetime)
        {
            _lifetime = lifetime;
            _lifetime.ApplicationStarted.Register(() => Console.WriteLine("[{0}]Application started", DateTimeOffset.Now));
            _lifetime.ApplicationStopping.Register(() => Console.WriteLine("[{0}]Application is stopping.", DateTimeOffset.Now));
            _lifetime.ApplicationStopped.Register(() => Console.WriteLine("[{0}]Application stopped.", DateTimeOffset.Now));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(5)).Token.Register(_lifetime.StopApplication);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _tokenSource?.Dispose();
            return Task.CompletedTask;
        }
    }
}