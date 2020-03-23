using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace App
{
    class Program
    {
        static void Main()
        {
            var listener = new DatabaseSourceListener();
            DatabaseSource.Instance.OnCommandExecute(CommandType.Text, "SELECT * FROM T_USER");
        }
    }
}
