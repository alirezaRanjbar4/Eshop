using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Warehouse
{
    public class WarehouseRepository : BaseRepository<WarehouseEntity>, IWarehouseRepository
    {
        public WarehouseRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
