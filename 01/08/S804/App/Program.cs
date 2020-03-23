using System.Data;

namespace App
{
    class Program
    {
        static void Main()
        => DatabaseSource.Instance.OnCommandExecute(CommandType.Text, "SELECT * FROM T_USER");
    }
}
