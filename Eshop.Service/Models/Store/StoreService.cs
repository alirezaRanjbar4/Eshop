using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Store;
using Eshop.Service.General;

namespace Eshop.Service.Models.Store
{
    public class StoreService : BaseService<StoreEntity>, IStoreService
    {
        public StoreService(IMapper mapper, IStoreRepository StoreRepository) : base(StoreRepository, mapper)
        {
        }
    }
}
