using HttpFileSystem;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Server
{
public class Program
{
    public static void Main()
    {
        Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder => builder.Configure(app=>app
                .UsePathBase("/files")
                .UseMiddleware<FileProviderMiddleware>(@"c:\test")))
                .Build()
                .Run();
    }
}
}
