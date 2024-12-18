using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ProductImage
{
    public class ProductImageRepository : BaseRepository<ProductImageEntity>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
