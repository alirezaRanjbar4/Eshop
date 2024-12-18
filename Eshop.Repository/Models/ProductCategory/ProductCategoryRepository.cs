using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ProductCategory
{
    public class ProductCategoryRepository : BaseRepository<ProductCategoryEntity>, IProductCategoryRepository
    {
        public ProductCategoryRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
