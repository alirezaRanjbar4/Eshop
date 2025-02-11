using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductPrice
{
    public interface IProductPriceService : IBaseService<ProductPriceEntity>, IScopedDependency
    {
    }
}
