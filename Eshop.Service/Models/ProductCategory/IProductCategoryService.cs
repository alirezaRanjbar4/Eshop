using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductCategory
{
    public interface IProductCategoryService : IBaseService<ProductCategoryEntity>, IScopedDependency
    {
    }
}
