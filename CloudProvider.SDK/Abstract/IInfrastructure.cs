using System;
using CloudProvider.SDK.IoC;
using CloudProvider.SDK.Persistence.Domain;

namespace CloudProvider.SDK.Abstract
{
    public interface IInfrastructure : IService<CloudInfrastructure, object, string>, IInjectable
    {
    }
}
