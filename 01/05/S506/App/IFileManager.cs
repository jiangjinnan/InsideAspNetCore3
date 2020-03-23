using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);
        Task<string> ReadAllTextAsync(string path);
    }
}
