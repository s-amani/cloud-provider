using System;
using CloudProvider.SDK.IoC;
using CloudProvider.SDK.Persistence.Domain;

namespace CloudProvider.SDK.Abstract
{
    public interface ICloudProvider : IService<Provider, string, string>, IInjectable
    {   
    }
}
