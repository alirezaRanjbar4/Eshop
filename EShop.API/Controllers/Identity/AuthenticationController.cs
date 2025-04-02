using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.Identities.Authentication;
using Eshop.DTO.Identities.User;
using Eshop.Service.FileStorage.Interface;
using Eshop.Service.Identity.Authentication.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Identity
{
    [ApiVersion(VersionProperties.V1)]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(
            IAuthenticationService authService)
        {
            _authService = authService;
        }


        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<LoginDTO> Login([FromBody] Login login, CancellationToken cancellationToken)
        {
            return await _authService.Login(login, HttpContext, cancellationToken);
        }


        [AllowAnonymous]
        [HttpPost(nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword resetPassword) =>
            Ok(await _authService.ResetPassword(resetPassword));


        [HttpGet(nameof(Logout))]
        public async Task<OperationResult<string>> Logout()
        {
            return await _authService.Logout(HttpContext);
        }

    }
}
