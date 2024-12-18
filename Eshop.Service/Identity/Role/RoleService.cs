using AutoMapper;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Authorization;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.Role;
using Eshop.Entity.Identities;
using Eshop.Repository.Identities.Role;
using Eshop.Service.General;
using Eshop.Service.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service.Identity.Role
{
    public class RoleService : BaseService<RoleEntity>, IRoleService
    {
        public readonly IMvcActionsDiscoveryService _mvcActionsDiscovery;
        private RoleManager<RoleEntity> _roleManager;
        public RoleService(RoleManager<RoleEntity> roleManager, IMapper mapper, IRoleRepository roleRepository, IMvcActionsDiscoveryService mvcActionsDiscovery) : base(roleRepository, mapper)
        {
            _roleManager = roleManager;
            _mvcActionsDiscovery = mvcActionsDiscovery;
        }


        public async Task<OperationResult<List<RoleDTO>>> GetAll(BaseSearchDTO baseSearch, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles
                             .Where(x => !x.Deleted && (string.IsNullOrEmpty(baseSearch.SearchTerm) || x.Name.Contains(baseSearch.SearchTerm)))
                             .OrderBy(o => o.Name)
                             .Skip((baseSearch.Page - 1) * baseSearch.PageSize)
                             .Take(baseSearch.PageSize)
                             .ToListAsync(cancellationToken);

            var totalRecord = await _roleManager.Roles.Where(x => !x.Deleted && (string.IsNullOrEmpty(baseSearch.SearchTerm) || x.Name.Contains(baseSearch.SearchTerm))).OrderBy(o => o.Name).CountAsync(cancellationToken);
            var dtos = _mapper.Map<List<RoleDTO>>(roles);

            foreach (var item in dtos)
            {
                var roleResult = await _roleManager.Roles.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.UserRoles.Any(x => x.RoleId == item.Id));
                item.Quantity = roleResult != null ? roleResult.UserRoles.Count() : 0;
            }

            return new OperationResult<List<RoleDTO>>
            {
                Data = dtos,
                TotalRecords = totalRecord,
            };
        }

        public async Task<RoleDTO> Get(Guid id, CancellationToken cancellationToken)
        {
            var existing = await _roleManager.Roles.Include(x => x.UserRoles).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            var dto = _mapper.Map<RoleDTO>(existing);
            var claims = await _roleManager.GetClaimsAsync(existing);
            dto.Claims = _mapper.Map<List<ClaimDTO>>(claims);

            return dto;
        }

        public async Task<OperationResult<Guid>> Add(AddRoleDTO role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                return new OperationResult<Guid>
                {
                    Data = Guid.Empty,
                    Message = "role already exists",
                };
            }

            var newRole = _mapper.Map<RoleEntity>(role);
            var res = await _roleManager.CreateAsync(newRole);
            if (!res.Succeeded)
            {
                return new OperationResult<Guid>
                {
                    Data = Guid.Empty,
                    Errors = res.Errors,
                };
            }

            return new OperationResult<Guid>
            {
                Data = newRole.Id,
                Message = "Ok",
            };
        }

        public async Task<OperationResult<bool>> Update(Guid id, AddRoleDTO role)
        {
            var existing = await _roleManager.FindByIdAsync(role.Id.ToString());

            if (existing == null)
                return new OperationResult<bool>
                {
                    Data = false,
                    Message = "role not found",
                };

            if (existing.NormalizedName == "ADMINISTRATOR")
            {
                return new OperationResult<bool>
                {
                    Data = false,
                    Message = "The Administrator cannot be updated.",
                };
            }

            existing.Name = role.Name;
            var res = await _roleManager.UpdateAsync(existing);

            // make sure it worked
            if (!res.Succeeded)
                return new OperationResult<bool>
                {
                    Data = false,
                    Message = "Error",
                    Errors = res.Errors,
                };

            return new OperationResult<bool> { Data = true };
        }

        public async Task<OperationResult<bool>> DeleteRole(Guid roleid)
        {
            var existing = await _roleManager.FindByIdAsync(roleid.ToString());
            if (existing == null)
            {
                return new OperationResult<bool>
                {
                    Data = false,
                    Message = "role not found",
                };
            }
            if (existing.NormalizedName == "ADMINISTRATOR")
            {
                return new OperationResult<bool>
                {
                    Data = false,
                    Message = "The Administrator cannot be removed."
                };
            }

            var result = await _roleManager.DeleteAsync(existing);

            if (result.Succeeded)
                return new OperationResult<bool> { Data = true };

            return new OperationResult<bool> { Data = false, Errors = result.Errors };
        }


        public List<ClaimDTO> GetClaims()
        {
            return _mapper.Map<List<ClaimDTO>>(SystemClaims.GetClaims());
        }

        public async Task<GetRoleDTO> FindClaimsInRole(Guid roleId, CancellationToken cancellationToken)
        {
            var findRole = await _roleManager.Roles.Include(c => c.RoleClaims.Where(i => !i.Deleted)).FirstOrDefaultAsync(c => c.Id == roleId, cancellationToken);
            return _mapper.Map<GetRoleDTO>(findRole);
        }

        public async Task<IdentityResult> AddOrUpdateClaimsAsync(Guid roleId, string roleClaimType, IList<string> selectedRoleClaimValues)
        {
            // حذف مقادیر خالی از لیست
            selectedRoleClaimValues = selectedRoleClaimValues.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            // پیدا کردن نقش
            var findRole = await _roleManager.Roles.Include(c => c.RoleClaims).FirstOrDefaultAsync(c => c.Id == roleId);
            if (findRole == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "NotFound",
                    Description = "کاربر مورد نظر یافت نشد.",
                });
            }

            var currentRoleClaimValues = findRole.RoleClaims
                .Where(r => r.ClaimType == roleClaimType && !r.Deleted)
                .Select(r => r.ClaimValue)
                .ToList();

            // پیدا کردن مقادیر جدید برای اضافه کردن
            var newClaimValuesToAdd = selectedRoleClaimValues
                .Except(currentRoleClaimValues, StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var claim in newClaimValuesToAdd)
            {
                var findRoleClaim = findRole.RoleClaims
                    .FirstOrDefault(x => x.ClaimValue.Equals(claim, StringComparison.OrdinalIgnoreCase) && x.ClaimType == roleClaimType);

                if (findRoleClaim == null)
                {
                    string[] parts = claim.Split(':');
                    findRole.RoleClaims.Add(new RoleClaimEntity
                    {
                        RoleId = roleId,
                        ClaimType = roleClaimType,
                        ClaimValue = claim,
                        RoutePath = parts.Length > 1 ? parts[1] : string.Empty,
                    });
                }
                else if (findRoleClaim.Deleted)
                {
                    findRoleClaim.Deleted = false;
                }
            }

            // پیدا کردن مقادیر برای حذف
            var removedClaimValues = currentRoleClaimValues
                .Except(selectedRoleClaimValues, StringComparer.OrdinalIgnoreCase)
                .ToList();

            foreach (var claim in removedClaimValues)
            {
                var roleClaim = findRole.RoleClaims
                    .Where(r => r.ClaimValue.Equals(claim, StringComparison.OrdinalIgnoreCase) && r.ClaimType == roleClaimType);

                if (roleClaim != null)
                    for (int i = 0; i < roleClaim.Count(); i++)
                    {
                        findRole.RoleClaims.Remove(roleClaim.ElementAt(i));

                    }
            }

            return await _roleManager.UpdateAsync(findRole);
        }

        public async Task<DynamicAccessDTO> GetRolePermission([FromBody] Guid roleId, CancellationToken cancellationToken)
        {
            if (roleId == Guid.Empty)
                return null;

            var role = await FindClaimsInRole(roleId,cancellationToken);
            if (role == null)
                return null;

            var securedControllerActions = _mvcActionsDiscovery.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission)
                  .Select(controllerViewModel =>
                  {
                      // ایجاد یک نسخه از ControllerViewModel بدون ControllerAttributes
                      var modifiedControllerViewModel = new ControllerDTO
                      {
                          AreaName = controllerViewModel.AreaName,
                          ControllerDisplayName = controllerViewModel.ControllerDisplayName,
                          ControllerName = controllerViewModel.ControllerName,
                          ControllerId = controllerViewModel.ControllerId,
                          ActionDetails = controllerViewModel.MvcActions.Select(actionDetails =>
                          {
                              var action = new ActionDetailsDTO
                              {
                                  ActionName = actionDetails.ActionName,
                                  ActionId = actionDetails.ActionId,
                                  ActionDisplayName = actionDetails.ActionDisplayName,
                                  ControllerId = actionDetails.ControllerId,
                              };
                              return action;
                          }).ToList()
                      };

                      var controllerAttributes = controllerViewModel.ControllerAttributes;
                      return modifiedControllerViewModel;
                  }).ToList();

            return new DynamicAccessDTO
            {
                RoleId = role.Id,
                RoleName = role.Name,
                SecuredControllerActions = securedControllerActions,
                RoleClaims = role.RoleClaims,
                BaseRoleClaims = role.BaseRoleClaim
            };
        }

    }
}
