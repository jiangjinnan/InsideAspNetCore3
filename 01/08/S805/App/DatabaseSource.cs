using System.Data;
using System.Diagnostics.Tracing;

namespace App
{
    [EventSource(Name = "Artech-Data-SqlClient")]
    public sealed class DatabaseSource : EventSource
    {
        public static readonly DatabaseSource Instance = new DatabaseSource();
        private DatabaseSource() { }
        [Event(1)]
        public void OnCommandExecute(CommandType commandType, string commandText) => WriteEvent(1, commandType, commandText);
    }
}