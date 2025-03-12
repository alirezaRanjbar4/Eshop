using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Customer
{
    public interface ICustomerService : IBaseService<CustomerEntity>, IScopedDependency
    {
        //Task<bool> AddCustomer(CustomerUserDTO customerUser, CancellationToken cancellationToken);
        //Task<bool> UpdateCustomer(CustomerUserDTO customerUser, CancellationToken cancellationToken);
    }
}
