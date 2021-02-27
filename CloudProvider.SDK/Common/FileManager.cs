using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Hosting;

namespace CloudProvider.SDK.Common
{
    public class FileManager : IFileManager
    {
        #region Fields

        public string CloudRootFolder {get; private set;}

        #endregion

        #region Ctor

        public FileManager(CloudConfiguration cloudConfigs, IHostingEnvironment env)
        {
            env.ThrowExceptionIfNull(nameof(env));
            cloudConfigs.ThrowExceptionIfNull(nameof(cloudConfigs));

            CloudRootFolder = $"{env.ContentRootPath}{cloudConfigs.ProviderRootDirectory}";
        }

        #endregion

        #region Methods

        public bool Exists(string name, bool lookupForFile = false)
        {
            var directoryName = $"{CloudRootFolder}/{name}";

            return lookupForFile ? File.Exists(directoryName) : Directory.Exists(directoryName);
        }

        public string CreateDirectory(string name)
        {
            var directoryName = $"{CloudRootFolder}/{name}";

            var info = Directory.CreateDirectory(directoryName);

            return info.FullName;
        }

        public string FindDirectory(string name)
        {
            var directoryName = $"{CloudRootFolder}/{name}";

            var directories = Directory.GetDirectories(directoryName);

            if (directories.Any())
                return directories.FirstOrDefault();

            return null;
        }

        public List<string> GetDirectories(string name)
        {
            var directoryName = $"{CloudRootFolder}/{name}";

            var directories = Directory.GetDirectories(directoryName);

            return directories.ToList();
        }

        public bool AnyFileOrDirectoryExists(string name)
        {
            var directoryName = $"{CloudRootFolder}/{name}";

            return Directory.GetDirectories(directoryName).Any() || Directory.GetFiles(directoryName).Any();
        }

        public void Delete(string name, bool deleteFile = false, string pattern = null)
        {
            if (deleteFile)
            {
                if (string.IsNullOrEmpty(pattern))
                {
                    File.Delete(name);
                }
                else
                {
                    var files = Directory.GetFiles(name, pattern).ToList();
                    files.ForEach(f => File.Delete(f));
                }

                return;
            }

            Directory.Delete(name);
        }

        #endregion
    }
}
