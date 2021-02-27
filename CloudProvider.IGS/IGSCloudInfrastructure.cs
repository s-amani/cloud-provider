using System;
using CloudProvider.SDK.Abstract;
using CloudProvider.SDK.Common;
using CloudProvider.SDK.Persistence.Provider;
using CloudProvider.SDK.Service;


namespace CloudProvider.IGS
{
    public class IGSCloudInfrastructure : Infrastructure, IIGSCloudInfrastructure
    {
        public IGSCloudInfrastructure(
            ICloudProvider cloudProvider, 
            IResourceProvider resourceProvider, 
            IFileManager fileManager, 
            IFileProvider fileProvider) : 
                base(cloudProvider, resourceProvider, fileManager, fileProvider)
        {
            fileManager.ThrowExceptionIfNull(nameof(fileManager));
            fileProvider.ThrowExceptionIfNull(nameof(fileProvider));
            cloudProvider.ThrowExceptionIfNull(nameof(cloudProvider));
            resourceProvider.ThrowExceptionIfNull(nameof(resourceProvider));
        }

        public override string ProviderName => "IGS";
    }
}
