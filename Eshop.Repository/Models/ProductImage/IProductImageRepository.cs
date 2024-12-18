using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ProductImage
{
    public interface IProductImageRepository : IBaseRepository<ProductImageEntity>, IScopedDependency
    {
    }
}
