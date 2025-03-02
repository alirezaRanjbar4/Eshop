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


        public async Task<bool> AddVendor(VendorUserDTO vendor, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<AddUserDTO>(vendor);
            var addUserResult = await _userService.AddUser(user, cancellationToken);
            if (!addUserResult.Data)
                return false;

            vendor.UserId = user.Id;
            var result = await AddAsync(vendor, true, cancellationToken);
            return result != null;
        }

        public async Task<bool> UpdateVendor(VendorUserDTO vendor, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<EditUserDTO>(vendor);
            var addUserResult = await _userService.EditUser(user, cancellationToken);
            if (!addUserResult.Data)
                return false;

            var result = await UpdateAsync(vendor, true, true, cancellationToken);
            return result != null;
        }
    }
}
