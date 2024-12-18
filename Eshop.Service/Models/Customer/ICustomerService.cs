using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Customer
{
    public interface ICustomerService : IBaseService<CustomerEntity>, IScopedDependency
    {
    }
}
