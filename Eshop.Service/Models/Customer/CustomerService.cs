using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Customer;
using Eshop.Service.General;

namespace Eshop.Service.Models.Customer
{
    public class CustomerService : BaseService<CustomerEntity>, ICustomerService
    {
        public CustomerService(IMapper mapper, ICustomerRepository CustomerRepository) : base(CustomerRepository, mapper)
        {
        }
    }
}
