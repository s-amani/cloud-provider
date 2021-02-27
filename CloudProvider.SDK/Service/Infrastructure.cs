using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudProvider.SDK.Abstract;
using CloudProvider.SDK.Common;
using CloudProvider.SDK.Persistence.Domain;
using CloudProvider.SDK.Persistence.Provider;

namespace CloudProvider.SDK.Service
{
    public abstract class Infrastructure : IInfrastructure
    {
        #region Fields

        protected readonly ICloudProvider _cloudProvider;

        protected readonly IResourceProvider _resourceProvider;

        protected readonly IFileManager _fileManager;

        protected readonly IFileProvider _fileProvider;

        #endregion

        #region Ctor

        public Infrastructure(
            ICloudProvider cloudProvider,
            IResourceProvider resourceProvider,
            IFileManager fileManager,
            IFileProvider fileProvider)
        {
            fileManager.ThrowExceptionIfNull(nameof(fileManager));
            fileProvider.ThrowExceptionIfNull(nameof(fileProvider));
            cloudProvider.ThrowExceptionIfNull(nameof(cloudProvider));
            resourceProvider.ThrowExceptionIfNull(nameof(resourceProvider));

            _cloudProvider = cloudProvider;
            _resourceProvider = resourceProvider;
            _fileManager = fileManager;
            _fileProvider = fileProvider;
        }

        #endregion

        #region Abstracts

        public abstract string ProviderName { get; }

        #endregion

        #region Public Methods

        public virtual async Task<object> Create(CloudInfrastructure infrastructure)
        {
            if (string.IsNullOrEmpty(ProviderName))
                throw new ArgumentNullException(nameof(ProviderName));

            // create provider
            await _cloudProvider.Create(new Provider { Name = ProviderName, Data = new { CreatedOn = DateTime.Now } });

            // create infrastructure
            var infraDirectory = await CreateInfrastructure(infrastructure);

            // create requested resources
            await CreateResources(infraDirectory, infrastructure);

            return Task.FromResult(true);
        }

        public virtual void Delete(string name)
        {
            var infraDirectory = Path.Combine(ProviderName, name);
            var directories = _fileManager.GetDirectories(infraDirectory);

            if (directories == null)
                return;

            // delete resources first regarding to dependency hierarchy
            directories.ForEach(resDirectory => _resourceProvider.Delete(resDirectory));

            DeleteInfrastructure(infraDirectory);
        }

        #endregion

        #region Helper Methods

        private async Task<string> CreateInfrastructure(CloudInfrastructure infrastructure)
        {
            var infraDirectory = Path.Combine(ProviderName, infrastructure.Name);

            if (_fileManager.Exists(infraDirectory))
            {
                throw new Exception($"{infrastructure.Name} infrastructure already created.");
            }

            // create infra folder
            var infraFullDirectory = _fileManager.CreateDirectory(infraDirectory);

            // create and save config file
            var fileToSave = $"{infraFullDirectory}/{infrastructure.Name}_config.json";

            // we dont want to create empty config file
            if (infrastructure.Data == null)
                return infraFullDirectory;

            var result = await _fileProvider.Save(fileToSave, infrastructure.Data);
            // ================

            if (!result)
            {
                // rollback and delete provider and infrastructure folder

                throw new Exception("Couldn't create infrastructure config file.");
            }

            return infraFullDirectory;
        }

        private async Task CreateResources(string path, CloudInfrastructure infrastructure)
        {
            var resourcesRootDirectory = Path.Combine(ProviderName, infrastructure.Name);

            // create resources for each requested resource
            var tasks = infrastructure.Resources.Select(res => CreateResource(resourcesRootDirectory, infrastructure.Name, res));

            await Task.WhenAll(tasks);
        }

        private async Task<string> CreateResource(string path, string infrastructureName, Resource resource)
        {
            // set some default values base on user inputs
            if (string.IsNullOrEmpty(resource.Path))
            {
                resource.Path = path;
            }

            if (string.IsNullOrEmpty(resource.Name))
            {
                // if name not provided we use infrastructure name for config file
                resource.Name = infrastructureName;
            }

            return await _resourceProvider.Create(resource);
        }

        private void DeleteInfrastructure(string path)
        {
            // delete infrastructure's config file
            var infraRootDirectoryPath = Path.Combine(_fileManager.CloudRootFolder, path);

            _fileManager.Delete(infraRootDirectoryPath, true, "*.json");

            // throw if there are any folder or file exists
            if (_fileManager.AnyFileOrDirectoryExists(path))
            {
                throw new Exception("Can not delete infrastructure folder.");
            }

            // delete infrastructure folder
            _fileManager.Delete(infraRootDirectoryPath);
        }

        #endregion
    }
}
