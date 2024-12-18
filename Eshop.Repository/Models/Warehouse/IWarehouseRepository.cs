using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Warehouse
{
    public interface IWarehouseRepository : IBaseRepository<WarehouseEntity>, IScopedDependency
    {
    }
}
