using Eshop.Application.DTO.Models.Vendor;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.Vendor
{
    public interface IVendorService : IBaseService<VendorEntity>, IScopedDependency
    {
        Task<bool> AddVendor(VendorUserDTO vendor, CancellationToken cancellationToken);
        Task<bool> UpdateVendor(VendorUserDTO vendor, CancellationToken cancellationToken);
    }
}
