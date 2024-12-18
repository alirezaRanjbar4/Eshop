using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.WarehouseLocation
{
    public interface IWarehouseLocationService : IBaseService<WarehouseLocationEntity>, IScopedDependency
    {
    }
}
