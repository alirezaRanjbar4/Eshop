using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Warehouse
{
    public interface IWarehouseService : IBaseService<WarehouseEntity>, IScopedDependency
    {
        Task<List<WarehouseInventoryDTO>> GetWarehouseInventory(Guid warehouseId, CancellationToken cancellationToken);
    }
}
