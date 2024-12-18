using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Store
{
    public class StoreRepository : BaseRepository<StoreEntity>, IStoreRepository
    {
        public StoreRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
