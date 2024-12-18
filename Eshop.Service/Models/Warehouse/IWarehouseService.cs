using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Warehouse
{
    public interface IWarehouseService : IBaseService<WarehouseEntity>, IScopedDependency
    {
    }
}
