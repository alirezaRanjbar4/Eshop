using Asp.Versioning;
using Eshop.Application.DTO.Identities.User;
using Eshop.Application.Service.Identity.User;
using Eshop.Presentation.Components;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Identity
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName(nameof(User))]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost(nameof(AddUser)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<OperationResult<bool>> AddUser([FromBody] AddUserDTO dto, CancellationToken cancellationToken)
        {
            return await _userService.AddUser(dto, cancellationToken);
        }


        [HttpPut(nameof(EditUser)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<OperationResult<bool>> EditUser([FromForm] AddUserDTO userDTO, CancellationToken cancellationToken)
        {
            return await _userService.EditUser(userDTO, cancellationToken);
        }

    }
}