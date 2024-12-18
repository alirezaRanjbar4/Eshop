using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.CustomerStore;
using Eshop.Service.General;

namespace Eshop.Service.Models.CustomerStore
{
    public class CustomerStoreService : BaseService<CustomerStoreEntity>, ICustomerStoreService
    {
        public CustomerStoreService(IMapper mapper, ICustomerStoreRepository CustomerStoreRepository) : base(CustomerStoreRepository, mapper)
        {
        }
    }
}
