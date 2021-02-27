using System;
using System.Threading.Tasks;
using CloudProvider.SDK.IoC;

namespace CloudProvider.SDK.Persistence.Provider
{
    public interface IFileProvider : IInjectable
    {
        Task<bool> Save(string path, object data);
    }
}
