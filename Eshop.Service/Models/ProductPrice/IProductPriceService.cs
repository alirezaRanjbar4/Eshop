using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductPrice
{
    public interface IProductPriceService : IBaseService<ProductPriceEntity>, IScopedDependency
    {
        Task<bool> AddProductPrice(ProductPriceDTO product, CancellationToken cancellationToken);
    }
}
