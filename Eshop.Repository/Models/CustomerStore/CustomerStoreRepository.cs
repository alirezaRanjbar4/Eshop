using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.CustomerStore
{
    public class CustomerStoreRepository : BaseRepository<CustomerStoreEntity>, ICustomerStoreRepository
    {
        public CustomerStoreRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
