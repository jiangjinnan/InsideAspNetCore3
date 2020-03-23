using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static async Task Main()
        {
            var content = await new ServiceCollection()
                .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"c:\test"))
                .AddSingleton<IFileManager, FileManager>()
                .BuildServiceProvider()
                .GetService<IFileManager>()
                .ReadAllTextAsync("data.txt");

            Debug.Assert(content == File.ReadAllText(@"c:\test\data.txt"));
        }
    }
}
