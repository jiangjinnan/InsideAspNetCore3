using System;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.FileProviders;

namespace HttpFileSystem
{
    public class HttpFileInfo : IFileInfo
    {
        private readonly HttpClient _httpClient;

        public bool Exists { get; }
        public bool IsDirectory { get; }
        public DateTimeOffset LastModified { get; }
        public long Length { get; }
        public string Name { get; }
        public string PhysicalPath { get; }

        public HttpFileInfo(HttpFileDescriptor descriptor, HttpClient httpClient)
        {
            Exists = descriptor.Exists;
            IsDirectory = descriptor.IsDirectory;
            LastModified = descriptor.LastModified;
            Length = descriptor.Length;
            Name = descriptor.Name;
            PhysicalPath = descriptor.PhysicalPath;
            _httpClient = httpClient;
        }

        public Stream CreateReadStream()
        {
            var message = _httpClient.GetAsync(this.PhysicalPath).Result;
            return message.Content.ReadAsStreamAsync().Result;
        }
    }

}