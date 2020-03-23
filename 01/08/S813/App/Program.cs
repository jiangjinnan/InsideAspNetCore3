using System;
using System.Diagnostics;

namespace App
{
class Program
{
    static void Main()
    {
        _ = new PerformanceCounterListener();
        Console.Read();
    }
}
}
