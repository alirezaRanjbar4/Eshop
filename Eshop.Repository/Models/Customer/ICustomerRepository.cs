using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Customer
{
    public interface ICustomerRepository : IBaseRepository<CustomerEntity>, IScopedDependency
    {
    }
}
