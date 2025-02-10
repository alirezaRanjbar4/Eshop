using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ProductPrice;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductPrice
{
    public class ProductPriceService : BaseService<ProductPriceEntity>, IProductPriceService
    {
        public ProductPriceService(IMapper mapper, IProductPriceRepository ProductPriceRepository) : base(ProductPriceRepository, mapper)
        {
        }
    }
}
