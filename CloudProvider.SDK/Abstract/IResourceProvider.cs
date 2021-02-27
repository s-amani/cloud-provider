using System;
using CloudProvider.SDK.IoC;
using CloudProvider.SDK.Persistence.Domain;

namespace CloudProvider.SDK.Abstract
{
    public interface IResourceProvider : IService<Resource, string, string>, IInjectable
    {
    }
}
