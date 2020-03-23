using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        public static async Task Main()
        {
            var cache = new ServiceCollection()
                .AddMemoryCache(options =>
                {
                    options.SizeLimit = 10;
                    options.CompactionPercentage = 0.2;
                })
                .BuildServiceProvider()
                .GetRequiredService<IMemoryCache>();

            for (int i = 1; i <= 5; i++)
            {
                cache.Set(i, i.ToString(), new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.Low,
                    Size = 1
                });
            }
            for (int i = 6; i <= 10; i++)
            {
                cache.Set(i, i.ToString(), new MemoryCacheEntryOptions
                {
                    Priority = CacheItemPriority.Normal,
                    Size = 1
                });
            }

            cache.Set(11, "11", new MemoryCacheEntryOptions
            {
                Priority = CacheItemPriority.Normal,
                Size = 1
            });
            await Task.Delay(1000);

            Console.WriteLine("Key\tValue");
            Console.WriteLine("--------------------");
            for (int i = 1; i <= 11; i++)
            {
                Console.WriteLine($"{i}\t{cache.Get<string>(i) ?? "N/A"}");
            }
        }
    }
}
