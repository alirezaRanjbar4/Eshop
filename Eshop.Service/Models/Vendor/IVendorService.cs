using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Vendor
{
    public interface IVendorService : IBaseService<VendorEntity>, IScopedDependency
    {
    }
}
