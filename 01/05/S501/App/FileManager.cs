using Microsoft.Extensions.FileProviders;
using System;

namespace App
{
    public class FileManager : IFileManager
    {
        private readonly IFileProvider _fileProvider;
        public FileManager(IFileProvider fileProvider) => _fileProvider = fileProvider;
        public void ShowStructure(Action<int, string> render)
        {
            int indent = -1;
            Render("");
            void Render(string subPath)
            {
                indent++;
                foreach (var fileInfo in _fileProvider.GetDirectoryContents(subPath))
                {
                    render(indent, fileInfo.Name);
                    if (fileInfo.IsDirectory)
                    {
                        Render($@"{subPath}\{fileInfo.Name}".TrimStart('\\'));
                    }
                }
                indent--;
            }
        }
    }
}
