using System;
using System.Threading.Tasks;
using CloudProvider.SDK.Abstract;
using CloudProvider.SDK.Common;
using CloudProvider.SDK.Persistence.Domain;
using CloudProvider.SDK.Persistence.Provider;

namespace CloudProvider.SDK.Service
{
    public class CloudProvider : ProviderBase, ICloudProvider
    {

        #region Ctor

        public CloudProvider(
            IFileManager fileManager,
            IFileProvider fileProvider,
            CloudConfiguration cloudConfiguration)
                : base(fileManager, fileProvider, cloudConfiguration)
        {
        }

        #endregion

        #region Public Methods

        public async Task<string> Create(Provider provider)
        {
            var providerDirectoryPath = string.Empty;

            // check if provider exists
            if (!_fileManager.Exists(provider.Name))
            {
                // Create a directory for provider
                providerDirectoryPath = _fileManager.CreateDirectory(provider.Name);

                // create configuration file related to provider
                var fileToSave = $"{providerDirectoryPath}/{provider.Name}_config.json";
                var result = await _fileProvider.Save(fileToSave, provider.Data);

                if (!result)
                {
                    // rollback and delete created directory

                    throw new Exception("Couldn't create provider config file.");
                }
            }

            return providerDirectoryPath;
        }

        public void Delete(string key)
        {
            throw new Exception("Currently we don't have a plan to delete Provider itself");
        }

        #endregion
    }
}