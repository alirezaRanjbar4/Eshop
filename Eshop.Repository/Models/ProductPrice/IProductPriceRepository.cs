using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ProductPrice
{
    public interface IProductPriceRepository : IBaseRepository<ProductPriceEntity>, IScopedDependency
    {
    }
}
