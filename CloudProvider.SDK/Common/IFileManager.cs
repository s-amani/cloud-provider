using System;
using System.Collections.Generic;
using CloudProvider.SDK.IoC;

namespace CloudProvider.SDK.Common
{
    public interface IFileManager : IInjectable
    {
        #region Properties

        string CloudRootFolder { get; }

        #endregion

        #region Methods

        bool Exists(string name, bool lookupForFile = false);
        string CreateDirectory(string name);
        string FindDirectory(string name);
        List<string> GetDirectories(string name);
        bool AnyFileOrDirectoryExists(string name);
        void Delete(string name, bool deleteFile = false, string pattern = null);

        #endregion
    }
}
