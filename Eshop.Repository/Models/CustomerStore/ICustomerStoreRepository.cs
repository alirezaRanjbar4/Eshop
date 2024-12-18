using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.CustomerStore
{
    public interface ICustomerStoreRepository : IBaseRepository<CustomerStoreEntity>, IScopedDependency
    {
    }
}
