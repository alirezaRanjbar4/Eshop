using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ProductPrice
{
    public class ProductPriceRepository : BaseRepository<ProductPriceEntity>, IProductPriceRepository
    {
        public ProductPriceRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
