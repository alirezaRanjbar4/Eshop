using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Customer;
using Eshop.Service.General;
using Eshop.Service.Models.CustomerStore;

namespace Eshop.Service.Models.Customer
{
    public class CustomerService : BaseService<CustomerEntity>, ICustomerService
    {
        private readonly ICustomerStoreService _customerStoreService;
        public CustomerService(IMapper mapper, ICustomerRepository CustomerRepository, ICustomerStoreService customerStoreService) : base(CustomerRepository, mapper)
        {
            _customerStoreService = customerStoreService;
        }

        public async Task<bool> AddCustomer(CustomerDTO customer,Guid storeId, CancellationToken cancellationToken)
        {
            var result = await AddAsync(customer, true, cancellationToken);

            if (result != null)
            {
                var customerStore = new CustomerStoreDTO
                {
                    CustomerId = result.Id,
                    StoreId = storeId
                };
                await _customerStoreService.AddAsync(customerStore, true, cancellationToken);

                return true;
            }

            return false;
        }
    }
}
