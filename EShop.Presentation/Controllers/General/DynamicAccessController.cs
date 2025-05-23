﻿using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Application.Service.Identity.Role;
using Eshop.Share.ActionFilters.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.General
{
    [Authorize]
    public class DynamicAccessController : BaseController
    {
        public readonly IRoleService _roleManger;
        public DynamicAccessController(IRoleService roleManager)
        {
            _roleManger = roleManager;
        }


        [HttpPost(nameof(GetRolePermission))]
        public async Task<DynamicAccessDTO> GetRolePermission([FromBody] Guid roleId, CancellationToken cancellationToken)
        {
            return await _roleManger.GetRolePermission(roleId, cancellationToken);
        }


        [HttpPost(nameof(AddRolePermission))]
        public async Task<OperationResult<bool>> AddRolePermission([FromBody] AddDynamicAccessDTO ViewModel)
        {
            var result = await _roleManger.AddOrUpdateClaimsAsync(ViewModel.RoleId, ConstantPolicies.DynamicPermissionClaimType, ViewModel.ActionIds.Split(","));
            if (!result.Succeeded)
                return new OperationResult<bool> { Data = false, Message = "در حین انجام عملیات خطایی رخ داده است." };

            return new OperationResult<bool> { Data = true, Message = "ثبت با موفقیت انجام شد" };
        }
    }
}
