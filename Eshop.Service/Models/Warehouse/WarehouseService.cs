using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Warehouse;
using Eshop.Service.General;

namespace Eshop.Service.Models.Warehouse
{
    public class WarehouseService : BaseService<WarehouseEntity>, IWarehouseService
    {
        public WarehouseService(IMapper mapper, IWarehouseRepository WarehouseRepository) : base(WarehouseRepository, mapper)
        {
        }
    }
}
