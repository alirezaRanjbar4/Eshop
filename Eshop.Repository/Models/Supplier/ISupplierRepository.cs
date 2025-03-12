using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Supplier
{
    public interface ISupplierRepository : IBaseRepository<SupplierEntity>, IScopedDependency
    {
    }
}
