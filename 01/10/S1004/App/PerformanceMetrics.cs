using System;

namespace App
{
    public class PerformanceMetrics
    {
        private static readonly Random _random = new Random();

        public int Processor { get; set; }
        public long Memory { get; set; }
        public long Network { get; set; }

        public override string ToString() => $"CPU: {Processor * 100}%; Memory: {Memory / (1024 * 1024)}M; Network: {Network / (1024 * 1024)}M/s";

        public static PerformanceMetrics Create() => new PerformanceMetrics
        {
            Processor = _random.Next(1, 8),
            Memory = _random.Next(10, 100) * 1024 * 1024,
            Network = _random.Next(10, 100) * 1024 * 1024
        };
    }
}