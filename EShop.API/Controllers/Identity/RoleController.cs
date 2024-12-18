using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.Core;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.Role;
using Eshop.Enum;
using Eshop.Service.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Base
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("Role")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpPost(nameof(GetAllRole)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<RoleDTO>>> GetAllRole([FromBody] BaseSearchDTO baseSearch, CancellationToken cancellationToken)
        {
            return await _roleService.GetAll(baseSearch, cancellationToken);
        }


        [HttpGet(nameof(GetById)), DisplayName(nameof(PermissionResourceEnums.GetPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<RoleDTO> GetById([FromQuery] Guid id, CancellationToken cancellatinToken)
        {
            return await _roleService.Get(id, cancellatinToken);
        }


        [HttpPost(nameof(AddRole)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult> AddRole([FromBody] AddRoleDTO role) =>
             Ok(await _roleService.Add(role));


        [HttpPut(nameof(UpdateRole)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult> UpdateRole([FromBody] AddRoleDTO role) =>
           Ok(await _roleService.Update(role.Id, role));


        [HttpDelete(nameof(DeleteRole)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult> DeleteRole(Guid roleid) =>
           Ok(await _roleService.DeleteRole(roleid));


        [HttpGet(nameof(GetClaims))]
        public IActionResult GetClaims() =>
           Ok(_roleService.GetClaims());

    }
}
