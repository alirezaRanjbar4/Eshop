using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Vendor
{
    public interface IVendorService : IBaseService<VendorEntity>, IScopedDependency
    {
        Task<bool> AddVendor(VendorUserDTO vendor, CancellationToken cancellationToken);
        Task<bool> UpdateVendor(VendorUserDTO vendor, CancellationToken cancellationToken);
    }
}
