using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Vendor
{
    public interface IVendorRepository : IBaseRepository<VendorEntity>, IScopedDependency
    {
    }
}
