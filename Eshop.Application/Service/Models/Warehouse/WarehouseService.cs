using AutoMapper;
using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Models.Warehouse;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Service.Models.Warehouse
{
    public class WarehouseService : BaseService<WarehouseEntity>, IWarehouseService
    {
        private readonly IBaseService<WarehouseLocationEntity> _warehouseLocationService;
        private readonly IBaseService<ProductWarehouseLocationEntity> _productWarehouseLocationService;
        public WarehouseService(
            IBaseService<WarehouseLocationEntity> warehouseLocationService,
            IBaseService<ProductWarehouseLocationEntity> productWarehouseLocationService,
            IMapper mapper,
            IBaseRepository<WarehouseEntity> WarehouseRepository) : base(WarehouseRepository, mapper)
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

            var deleteList = existedWarehouseLocations.Where(x => !dto.WarehouseLocations.Select(z => z.Id).Contains(x.Id));
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

        public async Task<List<SimpleDTO>> GetAllWarehouseLocation(Guid? productId, Guid storeId, CancellationToken cancellationToken)
        {
            return await _warehouseLocationService.GetAllAsync<SimpleDTO>(
                x => x.Warehouse.StoreId == storeId &&
                     (productId == Guid.Empty || productId == null || (x.ProductWarehouseLocations != null && x.ProductWarehouseLocations.Any(z => z.ProductId == productId && z.Count > 0))),
                i => i.Include(x => x.ProductWarehouseLocations).
                       Include(x => x.Warehouse),
                o => o.OrderBy(x => x.WarehouseId),
                false,
                cancellationToken);

        }
    }
}
