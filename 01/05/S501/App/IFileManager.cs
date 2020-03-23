using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public interface IFileManager
    {
        void ShowStructure(Action<int, string> render);
    }
}
