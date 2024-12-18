using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.WarehouseLocation;
using Eshop.Service.General;

namespace Eshop.Service.Models.WarehouseLocation
{
    public class WarehouseLocationService : BaseService<WarehouseLocationEntity>, IWarehouseLocationService
    {
        public WarehouseLocationService(IMapper mapper, IWarehouseLocationRepository WarehouseLocationRepository) : base(WarehouseLocationRepository, mapper)
        {
        }
    }
}
