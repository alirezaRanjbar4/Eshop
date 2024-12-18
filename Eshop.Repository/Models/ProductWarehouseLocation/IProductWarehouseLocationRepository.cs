using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.ProductWarehouseLocation
{
    public interface IProductWarehouseLocationRepository : IBaseRepository<ProductWarehouseLocationEntity>, IScopedDependency
    {
    }
}
