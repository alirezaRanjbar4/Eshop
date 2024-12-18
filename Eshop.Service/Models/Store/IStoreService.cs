using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Store
{
    public interface IStoreService : IBaseService<StoreEntity>, IScopedDependency
    {
    }
}
