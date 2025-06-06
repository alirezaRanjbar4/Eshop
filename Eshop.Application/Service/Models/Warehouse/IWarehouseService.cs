using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Models.Warehouse;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.Warehouse
{
    public interface IWarehouseService : IBaseService<WarehouseEntity>, IScopedDependency
    {
        Task<List<WarehouseInventoryDTO>> GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken);
        Task<bool> UpdateWarehouse(AddWarehouseDTO dto, CancellationToken cancellationToken);
        Task<List<SimpleDTO>> GetAllWarehouseLocation(Guid? productId, Guid storeId, CancellationToken cancellationToken);
    }
}
