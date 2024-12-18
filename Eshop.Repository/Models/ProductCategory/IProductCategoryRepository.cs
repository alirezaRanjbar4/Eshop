using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ProductCategory
{
    public interface IProductCategoryRepository : IBaseRepository<ProductCategoryEntity>, IScopedDependency
    {
    }
}
