using Eshop.Application.DTO.Models.Product;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.Product
{
    public interface IProductService : IBaseService<ProductEntity>, IScopedDependency
    {
        Task<bool> AddProduct(ProductDTO product, CancellationToken cancellationToken);
        Task<bool> UpdateProduct(ProductDTO product, CancellationToken cancellationToken);
    }
}
