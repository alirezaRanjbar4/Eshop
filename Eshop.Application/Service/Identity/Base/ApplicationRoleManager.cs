using Eshop.Application.DTO.Identities.Role;
using Eshop.Domain.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Eshop.Application.Service.Identity.Base
{
    public class ApplicationRoleManager : RoleManager<RoleEntity>, IApplicationRoleManager
    {
        private readonly IdentityErrorDescriber _errors;
        private readonly IApplicationUserManager _userManager;
        private readonly ILookupNormalizer _keyNormalizer;
        private readonly ILogger<ApplicationRoleManager> _logger;
        private readonly IEnumerable<IRoleValidator<RoleEntity>> _roleValidators;
        private readonly IRoleStore<RoleEntity> _store;

        public ApplicationRoleManager(
            IRoleStore<RoleEntity> store,
            ILookupNormalizer keyNormalizer,
            ILogger<ApplicationRoleManager> logger,
            IEnumerable<IRoleValidator<RoleEntity>> roleValidators,
            IdentityErrorDescriber errors,
            IApplicationUserManager userManager) :
            base(store, roleValidators, keyNormalizer, errors, logger)
        {
            //_errors = errors;
            //Common.Helpers.Utilities.Utilities.Utility.ObjectProvider.CheckArgumentIsNull(_errors, nameof(_errors));

            //_keyNormalizer = keyNormalizer;
            //Common.Helpers.Utilities.Utilities.Utility.ObjectProvider.CheckArgumentIsNull(_keyNormalizer, nameof(_keyNormalizer));

            //_logger = logger;
            // Common.Helpers.Utilities.Utilities.Utility.ObjectProvider.CheckArgumentIsNull(_logger,nameof(_logger));

            //_store = store;
            //Common.Helpers.Utilities.Utilities.Utility.ObjectProvider.CheckArgumentIsNull(_store, nameof(_store));

            //_roleValidators = roleValidators;
            //Common.Helpers.Utilities.Utilities.Utility.ObjectProvider.CheckArgumentIsNull(_roleValidators, nameof(_roleValidators));

            //_userManager = userManager;
            //Common.Helpers.Utilities.Utilities.Utility.ObjectProvider.CheckArgumentIsNull(_userManager, nameof(_userManager));
        }


        public List<RoleEntity> GetAllRoles()
        {
            return Roles.ToList();
        }


        public List<RoleDTO> GetAllRolesAndUsersCount()
        {
            return Roles.Select(role =>
                             new RoleDTO
                             {
                                 Id = role.Id,
                                 Name = role.Name,
                                 Quantity = role.UserRoles.Count(),
                             }).ToList();
        }

        public Task<RoleEntity> FindClaimsInRole(Guid roleId)
        {
            return Roles.Include(c => c.RoleClaims).FirstOrDefaultAsync(c => c.Id == roleId);
        }


        public async Task<IdentityResult> AddOrUpdateClaimsAsync(Guid roleId, string roleClaimType, IList<string> SelectedRoleClaimValues)
        {
            var Role = await FindClaimsInRole(roleId);
            if (Role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "NotFound",
                    Description = "نقش مورد نظر یافت نشد.",
                });
            }

            var CurrentRoleClaimValues = Role.RoleClaims.Where(r => r.ClaimType == roleClaimType).Select(r => r.ClaimValue).ToList();
            if (SelectedRoleClaimValues == null)
                SelectedRoleClaimValues = new List<string>();

            var NewClaimValuesToAdd = SelectedRoleClaimValues.Except(CurrentRoleClaimValues).ToList();
            foreach (var claim in NewClaimValuesToAdd)
            {
                Role.RoleClaims.Add(new RoleClaimEntity
                {
                    RoleId = roleId,
                    ClaimType = roleClaimType,
                    ClaimValue = claim,
                });
            }

            var removedClaimValues = CurrentRoleClaimValues.Except(SelectedRoleClaimValues).ToList();
            foreach (var claim in removedClaimValues)
            {
                var roleClaim = Role.RoleClaims.SingleOrDefault(r => r.ClaimValue == claim && r.ClaimType == roleClaimType);
                if (roleClaim != null)
                    Role.RoleClaims.Remove(roleClaim);
            }

            return await UpdateAsync(Role);
        }

        //public async Task<List<UsersViewModel>> GetUsersInRoleAsync(Guid RoleId)
        //{
        //    var userIds = (from r in Roles
        //                   where (r.Id == RoleId)
        //                   from u in r.UserRoles
        //                   select u.UserId).ToList();

        //    return await _userManager.Users.Where(user => userIds.Contains(user.Id))
        //        .Select(user => new User
        //        {
        //            Id = user.Id,
        //            Email = user.Email,
        //            UserName = user.UserName,
        //            PhoneNumber = user.PhoneNumber,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            BirthDate = user.BirthDate,
        //            IsActive = user.IsActive,
        //            Image = user.Image,
        //            RegisterDateTime = user.RegisterDateTime,
        //            Roles = user.Roles,
        //        }).AsNoTracking().ToListAsync();
        //}


        public async Task<List<RoleDTO>> GetPaginateRolesAsync(int offset, int limit, bool? roleNameSortAsc, string searchText)
        {
            List<RoleDTO> roles;
            roles = await Roles.Where(r => r.Name.Contains(searchText)).Select(role => new RoleDTO
            {
                Id = role.Id,
                Name = role.Name,
                Quantity = role.UserRoles.Count()
            }).Skip(offset).Take(limit).ToListAsync();

            if (roleNameSortAsc != null)
                roles = roles.OrderBy(t => roleNameSortAsc == true && roleNameSortAsc != null ? t.Name : "").OrderByDescending(t => roleNameSortAsc == false && roleNameSortAsc != null ? t.Name : "").ToList();



            return roles;
        }
    }
}
