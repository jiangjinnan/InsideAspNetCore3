using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.ObjectPool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    public class ConsolePublisher : IHealthCheckPublisher
    {
        private readonly ObjectPool<StringBuilder> _stringBuilderPool;

        public ConsolePublisher(ObjectPoolProvider provider)
        {
            _stringBuilderPool = provider.CreateStringBuilderPool();
        }

        public Task PublishAsync(HealthReport report, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var builder = _stringBuilderPool.Get();
            try
            {
                builder.AppendLine($"Status: {report.Status}[{ DateTimeOffset.Now.ToString("yy-MM-dd hh:mm:ss")}]");
                foreach (var name in report.Entries.Keys)
                {
                    builder.AppendLine($"    {name}: {report.Entries[name].Status}");
                }
                Console.WriteLine(builder);
                return Task.CompletedTask;
            }
            finally
            {
                _stringBuilderPool.Return(builder);
            }
        }
    }

}
