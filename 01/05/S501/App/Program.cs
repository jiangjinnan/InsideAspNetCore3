using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace App
{
    class Program
    {
        static void Main()
        {
            static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
            new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"c:\test"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetRequiredService<IFileManager>()
                .ShowStructure(Print);
        }
    }

}
