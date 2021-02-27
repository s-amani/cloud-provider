using System;
using System.Threading.Tasks;
using CloudProvider.SDK.Persistence.Domain;

namespace CloudProvider.SDK.Abstract
{
    public interface IService<in TCreateModel, TCreateReturn, TKey> where TCreateModel : BaseModel
    {
        Task<TCreateReturn> Create(TCreateModel model);

        void Delete(TKey key);
    }
}
