using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.WarehouseLocation
{
    public class WarehouseLocationRepository : BaseRepository<WarehouseLocationEntity>, IWarehouseLocationRepository
    {
        public WarehouseLocationRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
