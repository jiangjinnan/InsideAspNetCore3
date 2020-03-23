using HttpFileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace App
{
class Program
{
    static async Task Main()
    {
        var baseAddress = "http://localhost:5000/files/dir1";
        var fileManager = new ServiceCollection()
            .AddSingleton<IFileProvider>(new HttpFileProvider(baseAddress))
            .AddSingleton<IFileManager, FileManager>()
            .BuildServiceProvider()
            .GetRequiredService<IFileManager>();

        var content1 = await fileManager.ReadAllTextAsync("foobar/foo.txt");
        var content2 = await File.ReadAllTextAsync(@"c:\test\dir1\foobar\foo.txt");
        Debug.Assert(content1 == content2);
    }
}
}
