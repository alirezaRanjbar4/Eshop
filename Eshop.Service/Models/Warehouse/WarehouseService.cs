using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Warehouse;
using Eshop.Service.General;
using Eshop.Service.Models.ProductWarehouseLocation;
using Eshop.Service.Models.WarehouseLocation;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service.Models.Warehouse
{
    public class WarehouseService : BaseService<WarehouseEntity>, IWarehouseService
    {
        private readonly IWarehouseLocationService _warehouseLocationService;
        private readonly IProductWarehouseLocationService _productWarehouseLocationService;
        public WarehouseService(
            IMapper mapper,
            IWarehouseRepository WarehouseRepository,
            IWarehouseLocationService warehouseLocationService,
            IProductWarehouseLocationService productWarehouseLocationService) : base(WarehouseRepository, mapper)
        {
            _warehouseLocationService = warehouseLocationService;
            _productWarehouseLocationService = productWarehouseLocationService;
        }

        public async Task<List<WarehouseInventoryDTO>> GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken)
        {
            var result = new List<WarehouseInventoryDTO>();
            var warehouseLocations = await _warehouseLocationService.GetAllAsync<WarehouseLocationDTO>(x => x.WarehouseId == warehouseId, null, null, false, cancellationToken);
            foreach (var location in warehouseLocations)
            {
                var products = await _productWarehouseLocationService.GetAllAsync<WarehouseInventoryDTO>(
                    x => x.WarehouseLocationId == location.Id,
                    i => i.Include(x => x.Product),
                    null,
                    false,
                    cancellationToken);

                result.AddRange(products);
            }

            return result.OrderBy(x => x.Product).ToList();
        }
    }
}
