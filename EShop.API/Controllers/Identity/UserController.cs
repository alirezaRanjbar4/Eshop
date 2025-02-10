using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.User;
using Eshop.Enum;
using Eshop.Service.FileStorage.Interface;
using Eshop.Service.Identity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Identity
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName(nameof(User))]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IFileStorageService _fileStorageService;
        public UserController(IUserService userService, IFileStorageService fileStorageService)
        {
            _userService = userService;
            _fileStorageService = fileStorageService;
        }


        [HttpPost(nameof(GetUser)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public UserSearchDTO GetUser([FromBody] UserSearchInput req) =>
           _userService.SearchUsers(req);



        [HttpPost(nameof(AddUser)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<OperationResult<bool>> AddUser([FromBody] AddUserDTO dto, CancellationToken cancellationToken)
        {
            return await _userService.AddUser(dto, cancellationToken);
        }


        [HttpPut(nameof(EditUser)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<OperationResult<bool>> EditUser([FromForm] EditUserDTO userDTO, CancellationToken cancellationToken)
        {
            return await _userService.EditUser(userDTO, cancellationToken);
        }

    }
}