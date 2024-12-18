using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ProductWarehouseLocation;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductWarehouseLocation
{
    public class ProductWarehouseLocationService : BaseService<ProductWarehouseLocationEntity>, IProductWarehouseLocationService
    {
        public ProductWarehouseLocationService(IMapper mapper, IProductWarehouseLocationRepository ProductWarehouseLocationRepository) : base(ProductWarehouseLocationRepository, mapper)
        {
        }
    }
}
