using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.Role;
using Eshop.Entity.Identities;
using Eshop.Service.General;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Service.Identity.Role
{
    public interface IRoleService : IBaseService<RoleEntity>, IScopedDependency
    {
        Task<OperationResult<List<RoleDTO>>> GetAll(BaseSearchDTO baseSearch, CancellationToken cancellationToken);
        Task<IdentityResult> AddOrUpdateClaimsAsync(Guid roleId, string roleClaimType, IList<string> selectedRoleClaimValues);
        Task<RoleDTO> Get(Guid id, CancellationToken cancellationToken);
        Task<OperationResult<Guid>> Add(AddRoleDTO role);
        Task<OperationResult<bool>> Update(Guid id, AddRoleDTO role);
        Task<OperationResult<bool>> DeleteRole(Guid roleid);
        List<ClaimDTO> GetClaims();
        Task<GetRoleDTO> FindClaimsInRole(Guid roleId, CancellationToken cancellationToken);
        Task<DynamicAccessDTO> GetRolePermission(Guid roleId, CancellationToken cancellationToken);
    }
}
