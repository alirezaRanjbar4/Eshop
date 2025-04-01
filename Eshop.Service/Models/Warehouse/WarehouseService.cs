using AutoMapper;
using Eshop.DTO.Models.Warehouse;
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

        public async Task<bool> UpdateWarehouse(AddWarehouseDTO dto, CancellationToken cancellationToken)
        {
            var warehouse = _mapper.Map<WarehouseDTO>(dto);
            var updateResult = await UpdateAsync(warehouse, true, true, cancellationToken);

            var existedWarehouseLocations = await _warehouseLocationService.GetAllAsync<WarehouseLocationDTO>(x => x.WarehouseId == dto.Id, null, null, false, cancellationToken);

            var addList = dto.WarehouseLocations.Where(x => x.Id == Guid.Empty);
            await _warehouseLocationService.AddRangeAsync(addList, true, cancellationToken);

            var updateList = dto.WarehouseLocations.Where(x => x.Id != Guid.Empty);
            await _warehouseLocationService.UpdateRangeAsync(updateList, true, cancellationToken);

            var deleteList = existedWarehouseLocations.Where(x => dto.WarehouseLocations.Select(z => z.Id).Contains(x.Id));
            await _warehouseLocationService.DeleteRangeAsync(deleteList, true, true, cancellationToken);

            return true;

        }

        public async Task<List<WarehouseInventoryDTO>> GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken)
        {
            var result = new List<WarehouseInventoryDTO>();
            var warehouseLocations = await _warehouseLocationService.GetAllAsync<WarehouseLocationDTO>(x => x.WarehouseId == warehouseId, null, null, false, cancellationToken);
            foreach (var location in warehouseLocations)
            {
                var products = await _productWarehouseLocationService.GetAllAsync<WarehouseInventoryDTO>(
                    x => x.WarehouseLocationId == location.Id && x.Count != 0,
                    i => i.Include(x => x.Product).Include(x => x.WarehouseLocation),
                    null,
                    false,
                    cancellationToken);

                result.AddRange(products);
            }

            return result.OrderBy(x => x.Product).ToList();
        }
    }
}
