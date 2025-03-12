using AutoMapper;
using Eshop.DTO.Identities.User;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Vendor;
using Eshop.Service.General;
using Eshop.Service.Identity.User;
using Eshop.Service.Identity.UserRole;

namespace Eshop.Service.Models.Vendor
{
    public class VendorService : BaseService<VendorEntity>, IVendorService
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        public VendorService(
            IMapper mapper,
            IVendorRepository VendorRepository,
            IUserService userService,
            IUserRoleService userRoleService) : base(VendorRepository, mapper)
        {
            _userService = userService;
            _userRoleService = userRoleService;
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
            if (result == null)
                return false;

            var userRole = new UserRoleDTO
            {
                RoleId = vendorUser.RoleId,
                UserId = user.Id
            };
            var addUserRoleResult = await _userRoleService.AddAsync(userRole, true, cancellationToken);


            return addUserRoleResult != null;
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
            if (result == null)
                return false;

            var userRole = await _userRoleService.GetAsync<UserRoleDTO>(x => x.UserId == vendorUser.UserId, null, false, cancellationToken);
            if (userRole != null)
                await _userRoleService.DeleteAsync(userRole.Id, true, true, true, cancellationToken);

            var newuserRole = new UserRoleDTO() { UserId = user.Id, RoleId = vendorUser.RoleId };
            var editUserRoleResult = await _userRoleService.AddAsync(newuserRole, true, cancellationToken);

            return editUserRoleResult != null;
        }
    }
}
