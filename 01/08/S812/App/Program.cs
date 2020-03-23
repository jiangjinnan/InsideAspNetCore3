using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace App
{
    public class Program
    {
        static readonly Random _random = new Random();
        public static async Task Main()
        {
            File.AppendAllLines("log.csv",
                new string[] { "EventName,StartTime,Elapsed,ActivityId,RelatedActivityId" });
            var listener = new FoobarListener();
            await FooAsync();
        }

        static Task FooAsync() => InvokeAsync(FoobarSource.Instance.FooStart,
            FoobarSource.Instance.FooStop, async () =>
            {
                await BarAsync();
                await GuxAsync();
            });
        static Task BarAsync() => InvokeAsync(FoobarSource.Instance.BarStart,
            FoobarSource.Instance.BarStop, BazAsync);
        static Task BazAsync() => InvokeAsync(FoobarSource.Instance.BazStart,
            FoobarSource.Instance.BazStop, () => Task.CompletedTask);
        static Task GuxAsync() => InvokeAsync(FoobarSource.Instance.GuxStart,
            FoobarSource.Instance.GuxStop, () => Task.CompletedTask);

        static async Task InvokeAsync(Action<long> start, Action<double> stop, Func<Task> body)
        {
            start(Stopwatch.GetTimestamp());
            var sw = Stopwatch.StartNew();
            await Task.Delay(_random.Next(10, 100));
            await body();
            stop(sw.ElapsedMilliseconds);
        }
    }

}
