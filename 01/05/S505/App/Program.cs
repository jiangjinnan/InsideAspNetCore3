using HttpFileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;

namespace App
{
class Program
{
    static void Main()
    {
        var baseAddress = "http://localhost:5000/files/dir1";
        var fileManager = new ServiceCollection()
                .AddSingleton<IFileProvider>(new HttpFileProvider(baseAddress))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetService<IFileManager>();
        fileManager.ShowStructure((layer, name)=> Console.WriteLine($"{new string('\t', layer)}{name}"));
    }
}
}
