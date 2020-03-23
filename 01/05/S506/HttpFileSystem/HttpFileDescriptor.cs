using Microsoft.Extensions.FileProviders;
using System;
using System.Net.Http;

namespace HttpFileSystem
{
    public class HttpFileDescriptor
    {
        public bool Exists { get; set; }
        public bool IsDirectory { get; set; }
        public DateTimeOffset LastModified { get; set; }
        public long Length { get; set; }
        public string Name { get; set; }
        public string PhysicalPath { get; set; }

        public HttpFileDescriptor()
        { }

        public HttpFileDescriptor(IFileInfo fileInfo, Func<string, string> physicalPathResolver)
        {
            Exists = fileInfo.Exists;
            IsDirectory = fileInfo.IsDirectory;
            LastModified = fileInfo.LastModified;
            Length = fileInfo.Length;
            Name = fileInfo.Name;
            PhysicalPath = physicalPathResolver(fileInfo.Name);
        }

        public IFileInfo ToFileInfo(HttpClient httpClient)
        {
            return Exists
                ? new HttpFileInfo(this, httpClient)
                : (IFileInfo)new NotFoundFileInfo(this.Name);
        }
    }

}
