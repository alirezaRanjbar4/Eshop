using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Application.DTO.Identities.Role;
using Eshop.Application.Service.General;
using Eshop.Domain.Identities;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Helpers.Utilities.Interface;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Application.Service.Identity.Role
{
    public interface IRoleService : IBaseService<RoleEntity>, IScopedDependency
    {
        Task<List<SimpleRoleDTO>> GetAllRoles(CancellationToken cancellationToken);
        Task<IdentityResult> AddOrUpdateClaimsAsync(Guid roleId, string roleClaimType, IList<string> selectedRoleClaimValues);
        Task<OperationResult<Guid>> Add(AddRoleDTO role);
        Task<OperationResult<bool>> Update(Guid id, AddRoleDTO role);
        Task<OperationResult<bool>> DeleteRole(Guid roleid);
        List<ClaimDTO> GetClaims();
        Task<GetRoleDTO> FindClaimsInRole(Guid roleId, CancellationToken cancellationToken);
        Task<DynamicAccessDTO> GetRolePermission(Guid roleId, CancellationToken cancellationToken);
    }
}
