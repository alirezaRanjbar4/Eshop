using Eshop.DTO.Identities.Role;
using Eshop.Entity.Identities;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Service.Identity.Base
{
    public interface IApplicationRoleManager
    {
        #region BaseClass
        IQueryable<RoleEntity> Roles { get; }
        ILookupNormalizer KeyNormalizer { get; set; }
        IdentityErrorDescriber ErrorDescriber { get; set; }
        IList<IRoleValidator<RoleEntity>> RoleValidators { get; }
        bool SupportsQueryableRoles { get; }
        bool SupportsRoleClaims { get; }
        Task<IdentityResult> CreateAsync(RoleEntity role);
        Task<IdentityResult> DeleteAsync(RoleEntity role);
        Task<RoleEntity> FindByIdAsync(string roleId);
        Task<RoleEntity> FindByNameAsync(string roleName);
        string NormalizeKey(string key);
        Task<bool> RoleExistsAsync(string roleName);
        Task<IdentityResult> UpdateAsync(RoleEntity role);
        Task UpdateNormalizedRoleNameAsync(RoleEntity role);
        Task<string> GetRoleNameAsync(RoleEntity role);
        Task<IdentityResult> SetRoleNameAsync(RoleEntity role, string name);
        #endregion

        #region CustomMethod
        List<RoleEntity> GetAllRoles();
        List<RoleDTO> GetAllRolesAndUsersCount();
        Task<RoleEntity> FindClaimsInRole(Guid RoleId);
        //  Task<List<UsersViewModel>> GetUsersInRoleAsync(int RoleId);
        Task<IdentityResult> AddOrUpdateClaimsAsync(Guid RoleId, string RoleClaimType, IList<string> SelectedRoleClaimValues);
        Task<List<RoleDTO>> GetPaginateRolesAsync(int offset, int limit, bool? roleNameSortAsc, string searchText);
        #endregion
    }
}
