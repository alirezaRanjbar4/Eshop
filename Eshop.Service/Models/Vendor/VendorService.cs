using AutoMapper;
using Eshop.DTO.Identities.User;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Vendor;
using Eshop.Service.General;
using Eshop.Service.Identity.User;

namespace Eshop.Service.Models.Vendor
{
    public class VendorService : BaseService<VendorEntity>, IVendorService
    {
        private readonly IUserService _userService;
        public VendorService(
            IMapper mapper,
            IVendorRepository VendorRepository,
            IUserService userService) : base(VendorRepository, mapper)
        {
            _userService = userService;
        }


        public async Task<bool> AddVendor(VendorUserDTO vendorUser, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AddUserDTO>(vendorUser);
            var addUserResult = await _userService.AddUser(user, cancellationToken);
            if (!addUserResult.Data)
                return false;

            var vendor = _mapper.Map<VendorDTO>(vendorUser);
            vendor.UserId = user.Id;
            vendor.Id = Guid.Empty;
            var result = await AddAsync(vendor, true, cancellationToken);
            return result != null;
        }

        public async Task<bool> UpdateVendor(VendorUserDTO vendorUser, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<EditUserDTO>(vendorUser);
            user.Id = vendorUser.UserId;
            var editUserResult = await _userService.EditUser(user, cancellationToken);
            if (!editUserResult.Data)
                return false;

            var vendor = _mapper.Map<VendorDTO>(vendorUser);
            var result = await UpdateAsync(vendor, true, true, cancellationToken);
            return result != null;
        }
    }
}
