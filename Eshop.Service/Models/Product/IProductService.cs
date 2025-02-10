using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Product
{
    public interface IProductService : IBaseService<ProductEntity>, IScopedDependency
    {
        Task<bool> AddProduct(ProductDTO product, CancellationToken cancellationToken);
        Task<bool> UpdateProduct(ProductDTO product, CancellationToken cancellationToken);
    }
}
