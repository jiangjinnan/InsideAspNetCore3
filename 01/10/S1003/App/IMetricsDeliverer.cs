using System.Threading.Tasks;

namespace App
{
    public interface IMetricsDeliverer
    {
        Task DeliverAsync(PerformanceMetrics counter);
    }
}