using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.Role;
using Eshop.Service.Identity.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Models.Admin
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("AdminRole")]
    public class AdminRoleController : BaseController
    {
        private readonly IRoleService _roleService;
        public AdminRoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpGet(nameof(GetAllRoles))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<List<SimpleRoleDTO>> GetAllRoles(CancellationToken cancellationToken)
        {
            return await _roleService.GetAllRoles(cancellationToken);
        }


        [HttpPost(nameof(AddRole)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult> AddRole([FromBody] AddRoleDTO role) =>
             Ok(await _roleService.Add(role));


        [HttpPut(nameof(UpdateRole)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult> UpdateRole([FromBody] AddRoleDTO role) =>
           Ok(await _roleService.Update(role.Id, role));


        [HttpDelete(nameof(DeleteRole)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<ActionResult> DeleteRole(Guid roleid) =>
           Ok(await _roleService.DeleteRole(roleid));


        [HttpGet(nameof(GetClaims))]
        public IActionResult GetClaims() =>
           Ok(_roleService.GetClaims());

    }
}
