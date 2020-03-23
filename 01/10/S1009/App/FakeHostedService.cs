using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public class FakeHostedService : IHostedService
    {
        private readonly IHostEnvironment _environment;
        public FakeHostedService(IHostEnvironment environment) => _environment = environment;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("{0,-15}:{1}", nameof(_environment.EnvironmentName), _environment.EnvironmentName);
            Console.WriteLine("{0,-15}:{1}", nameof(_environment.ApplicationName), _environment.ApplicationName);
            Console.WriteLine("{0,-15}:{1}", nameof(_environment.ContentRootPath), _environment.ContentRootPath);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }

}