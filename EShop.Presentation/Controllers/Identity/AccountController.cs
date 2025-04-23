using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Application.DTO.Identities.User;
using Eshop.Application.Service.Identity.User;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Identity
{
    [Authorize]
    [DisplayName("Account")]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost(nameof(ChangeUserPassword)), DisplayName(nameof(PermissionResourceEnums.ChangeUserPassword))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.Success, ResultType = ResultType.Success)]
        public async Task<OperationResult<bool>> ChangeUserPassword([FromBody] ChangeUserPasswordDTO changeUserPasswordDTO, CancellationToken cancellationToken)
        {
            return await _userService.ChangeUserPassword(changeUserPasswordDTO, cancellationToken);
        }


        [HttpGet(nameof(GetUserById)), DisplayName(nameof(PermissionResourceEnums.GetPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<EditUserDTO> GetUserById([FromQuery] Guid userId, CancellationToken cancellationToken)
        {
            return await _userService.GetAsync<EditUserDTO>(x => x.Id == userId, null, false, cancellationToken);
        }


        [HttpPut(nameof(EditUser)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<OperationResult<bool>> EditUser([FromForm] AddUserDTO userDTO, CancellationToken cancellationToken)
        {
            return await _userService.EditUser(userDTO, cancellationToken);
        }
    }
}
