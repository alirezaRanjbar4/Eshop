using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Supplier
{
    public interface ISupplierService : IBaseService<SupplierEntity>, IScopedDependency
    {
    }
}
