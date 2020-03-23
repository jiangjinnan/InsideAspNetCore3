using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace App
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ConcurrentDictionary<string, IStringLocalizer> _localizers;
        private readonly IFileProvider _fileProvider;

        public JsonStringLocalizerFactory(IFileProvider fileProvider)
        {
            _localizers = new ConcurrentDictionary<string, IStringLocalizer>();
            _fileProvider = fileProvider;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            var path = ParseFilePath(resourceSource);
            return _localizers.GetOrAdd(path, _ =>
            {
                return CreateStringLocalizer(_);
            });
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var path = ParseFilePath(location, baseName);
            return _localizers.GetOrAdd(path, _ =>
            {
                return CreateStringLocalizer(_);
            });
        }

        private IStringLocalizer CreateStringLocalizer(string path)
        {
            var file = _fileProvider.GetFileInfo(path);
            if (!file.Exists)
            {
                return new DictionaryStringLocalizer(new Dictionary<string, LocalizedStringEntry>());
            }
            using (var stream = file.CreateReadStream())
            {
                var buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                var dictionary = (Dictionary<string, LocalizedStringEntry>)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(buffer), typeof(Dictionary<string, LocalizedStringEntry>));
                return new DictionaryStringLocalizer(dictionary);
            }
        }

        private string ParseFilePath(string location, string baseName)
        {
            var path = location + "." + baseName;
            return path
                .Replace("..", ".")
                .Replace('.', Path.DirectorySeparatorChar) + ".json";
        }

        private string ParseFilePath(Type resourceSource)
        {
            var rootNS = resourceSource.Assembly.GetCustomAttribute<RootNamespaceAttribute>()
                ?.RootNamespace ?? new AssemblyName(resourceSource.Assembly.FullName).Name;
            return resourceSource.FullName.StartsWith(rootNS)
                ? resourceSource.FullName.Substring(rootNS.Length + 1) + ".json"
                : resourceSource.FullName + ".json";
        }
    }

}