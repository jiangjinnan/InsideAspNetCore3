using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace App
{
    class Program
    {
        static void Main()
        {
            DiagnosticListener.AllListeners.Subscribe(new Observer<DiagnosticListener>( listener =>
            {
                if (listener.Name == "Artech-Data-SqlClient")
                {
                    listener.Subscribe(new Observer<KeyValuePair<string, object>>(eventData =>
                    {
                        Console.WriteLine($"Event Name: {eventData.Key}");
                        dynamic payload = eventData.Value;
                        Console.WriteLine($"CommandType: {payload.CommandType}");
                        Console.WriteLine($"CommandText: {payload.CommandText}");
                    }));
                }
            }));

            var source = new DiagnosticListener("Artech-Data-SqlClient");
            if (source.IsEnabled("CommandExecution"))
            {
                source.Write("CommandExecution",
                    new
                    {
                        CommandType = CommandType.Text,
                        CommandText = "SELECT * FROM T_USER"
                    });
            }
        }
    }
}
