using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Customer
{
    public interface ICustomerService : IBaseService<CustomerEntity>, IScopedDependency
    {
        Task<bool> AddCustomer(CustomerDTO customer,Guid storeId, CancellationToken cancellationToken);
    }
}
