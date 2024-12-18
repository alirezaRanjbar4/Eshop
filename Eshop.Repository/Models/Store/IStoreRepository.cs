using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Store
{
    public interface IStoreRepository : IBaseRepository<StoreEntity>, IScopedDependency
    {
    }
}
