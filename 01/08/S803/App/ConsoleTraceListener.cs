using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace App
{
    public class ConsoleTraceListener : TraceListener
    {
        public override void Write(string message) => Console.Write(message);
        public override void WriteLine(string message) => Console.WriteLine(message);
    }
}
