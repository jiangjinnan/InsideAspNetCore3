using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Threading.Tasks;

namespace App
{
public class Program
{
    public static async Task Main()
    {
        var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
        var fileName = "time.txt";
        var @lock = new object();            

        var cache = new ServiceCollection()
            .AddMemoryCache()
            .BuildServiceProvider()
            .GetRequiredService<IMemoryCache>();

        var options = new MemoryCacheEntryOptions();
        options.AddExpirationToken(fileProvider.Watch(fileName));
        options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration { EvictionCallback = OnEvicted });

        Write(DateTime.Now.ToString());
        cache.Set("CurrentTime", Read(), options);

        while (true)
        {
            Write(DateTime.Now.ToString());
            await Task.Delay(1000);
            if (cache.TryGetValue("CurrentTime", out string currentTime))
            {
                Console.WriteLine(currentTime);
            }
        }

        string Read()
        {
            lock (@lock)
            {
                return File.ReadAllText(fileName);
            }
        }

        void Write(string contents)
        {
            lock (@lock)
            {
                File.WriteAllText(fileName, contents);
            }
        }

        void OnEvicted(object key, object value, EvictionReason reason, object state)
        {
            options.ExpirationTokens.Clear();
            options.AddExpirationToken(fileProvider.Watch(fileName));
            cache.Set("CurrentTime", Read(), options);
        }
    }
}

}
