using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductImage
{
    public interface IProductImageService : IBaseService<ProductImageEntity>, IScopedDependency
    {
    }
}
