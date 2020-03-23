using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Tracing;
using System.Text;

namespace App
{
    [EventSource(Name = "Artech-Data-SqlClient")]
    public sealed class DatabaseSource : EventSource
    {
        public static DatabaseSource Instance = new DatabaseSource();
        private DatabaseSource() { }

        [Event(1, Level = EventLevel.Informational, Keywords = EventKeywords.None,Opcode = EventOpcode.Info, Task = Tasks.DA, Tags = Tags.MSSQL, Version = 1,
            Message = "Execute SQL command. Type: {0}, Command Text: {1}")]
        public void OnCommandExecute(CommandType commandType, string commandText)
        {
            if (IsEnabled(EventLevel.Informational, EventKeywords.All, EventChannel.Debug))
            {
                WriteEvent(1, (int)commandType, commandText);
            }
        }
    }
}
