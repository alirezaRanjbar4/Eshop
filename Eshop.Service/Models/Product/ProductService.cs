using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Product;
using Eshop.Service.General;

namespace Eshop.Service.Models.Product
{
    public class ProductService : BaseService<ProductEntity>, IProductService
    {
        public ProductService(IMapper mapper, IProductRepository ProductRepository) : base(ProductRepository, mapper)
        {
        }
    }
}
