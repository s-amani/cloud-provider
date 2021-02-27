using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using CloudProvider.SDK.Abstract;
using CloudProvider.SDK.Common;
using CloudProvider.SDK.Persistence.Domain;
using CloudProvider.SDK.Persistence.Provider;

namespace CloudProvider.SDK.Service
{
    public class ResourceProvider : ProviderBase, IResourceProvider
    {
        #region Ctor

        public ResourceProvider(
            IFileManager fileManager,
            IFileProvider fileProvider,
            CloudConfiguration cloudConfiguration)
                : base(fileManager, fileProvider, cloudConfiguration)
        {
        }

        #endregion

        #region Public Methods

        public virtual async Task<string> Create(Resource model)
        {
            string result;

            switch (model.ResourceType)
            {
                case ResourceType.VirtualMachine:
                    result = await CreateVirtualMachine(model);
                    break;

                case ResourceType.DatabaseServer:
                    result = await CreateDatabaseServer(model);
                    break;

                default:
                    throw new Exception("Not supported resource type.");
            }

            return result;
        }

        public virtual void Delete(string name)
        {
            // delete all json files first 
            _fileManager.Delete(name, true, "*.json");


            // delete folder
            _fileManager.Delete(name);
        }

        #endregion

        #region Helper Methods

        private async Task<string> CreateVirtualMachine(Resource resource)
        {
            var directoryName = CombineAndValidateDirectoryExistance(resource.Path, _cloudConfiguration.VirtualMachineFolder);

            var providerDirectoryPath = await CreateDirectoryAndSaveConfig(resource, directoryName);

            return providerDirectoryPath;
        }

        private async Task<string> CreateDatabaseServer(Resource resource)
        {
            var directoryName = CombineAndValidateDirectoryExistance(resource.Path, _cloudConfiguration.DatabaseServerFolder);
            
            var providerDirectoryPath = await CreateDirectoryAndSaveConfig(resource, directoryName);

            return providerDirectoryPath;
        }

        private string CombineAndValidateDirectoryExistance(params string[] path)
        {
            if (path.Length < 2)
                throw new ArgumentOutOfRangeException(nameof(path));

            var directoryName = Path.Combine(path[0], path[1]);

            if (_fileManager.Exists(directoryName))
            {
                throw new Exception($"{path[1]} already created in specified {path[2]} path.");
            }

            return directoryName;
        }

        private async Task<string> CreateDirectoryAndSaveConfig(Resource resource, string directoryName)
        {
            var providerDirectoryPath = _fileManager.CreateDirectory(directoryName);

            var fileToSave = $"{providerDirectoryPath}/{resource.Name.ToUpper(CultureInfo.CurrentCulture)}_SERVER.json";

            await _fileProvider.Save(fileToSave, resource.Data);

            return providerDirectoryPath;
        }

        #endregion
    }
}
