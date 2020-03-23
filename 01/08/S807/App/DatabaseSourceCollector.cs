using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace App
{
    public class DatabaseSourceCollector
    {
        [DiagnosticName("CommandExecution")]
        public void OnCommandExecute(CommandType commandType, string commandText)
        {
            Console.WriteLine($"Event Name: CommandExecution");
            Console.WriteLine($"CommandType: {commandType}");
            Console.WriteLine($"CommandText: {commandText}");
        }
    }
}
