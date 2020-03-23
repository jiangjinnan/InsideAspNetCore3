using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public sealed class FakeHostedService : IHostedService
    {
        public FakeHostedService(IFoo foo, IBar bar, IBaz baz)
        {
            Debug.Assert(foo != null);
            Debug.Assert(bar != null);
            Debug.Assert(baz != null);
        }
        public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}