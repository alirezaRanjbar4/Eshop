using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ProductWarehouseLocation
{
    public class ProductWarehouseLocationRepository : BaseRepository<ProductWarehouseLocationEntity>, IProductWarehouseLocationRepository
    {
        public ProductWarehouseLocationRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
