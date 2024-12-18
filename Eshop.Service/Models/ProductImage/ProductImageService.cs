using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ProductImage;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductImage
{
    public class ProductImageService : BaseService<ProductImageEntity>, IProductImageService
    {
        public ProductImageService(IMapper mapper, IProductImageRepository ProductImageRepository) : base(ProductImageRepository, mapper)
        {
        }
    }
}
