using System;
using CloudProvider.SDK.Common;
using CloudProvider.SDK.Persistence.Provider;

namespace CloudProvider.SDK.Service
{
    public class ProviderBase
    {
        #region Fields

        protected readonly IFileManager _fileManager;

        protected readonly IFileProvider _fileProvider;

        protected readonly CloudConfiguration _cloudConfiguration;

        #endregion

        #region Ctor

        public ProviderBase(IFileManager fileManager, IFileProvider fileProvider, CloudConfiguration cloudConfiguration)
        {
            fileManager.ThrowExceptionIfNull(nameof(fileManager));
            fileProvider.ThrowExceptionIfNull(nameof(fileProvider));
            cloudConfiguration.ThrowExceptionIfNull(nameof(cloudConfiguration));


            _fileManager = fileManager;
            _fileProvider = fileProvider;
            _cloudConfiguration = cloudConfiguration;
        }

        #endregion

    }
}
