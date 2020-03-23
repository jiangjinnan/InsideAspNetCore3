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
            DiagnosticListener.AllListeners.Subscribe(new Observer<DiagnosticListener>(listener => {
                    if (listener.Name == "Artech-Data-SqlClient")
                    {
                        listener.SubscribeWithAdapter(new DatabaseSourceCollector());
                    }
                }));

            var source = new DiagnosticListener("Artech-Data-SqlClient");
            source.Write("CommandExecution",new { CommandType = CommandType.Text, CommandText = "SELECT * FROM T_USER" });
        }
    }
}
