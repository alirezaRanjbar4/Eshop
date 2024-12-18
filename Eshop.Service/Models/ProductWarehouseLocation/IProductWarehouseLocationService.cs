using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.ProductWarehouseLocation
{
    public interface IProductWarehouseLocationService : IBaseService<ProductWarehouseLocationEntity>, IScopedDependency
    {
    }
}
