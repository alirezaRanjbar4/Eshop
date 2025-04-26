using Eshop.Application.DTO.Identities.Authentication;
using Eshop.Application.DTO.Identities.User;
using Eshop.Application.Service.Identity.Authentication;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Identity
{
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
        [HttpPost(nameof(RefreshToken))]
        public async Task<LoginDTO> RefreshToken([FromBody] TokenRequestDTO refreshToken, CancellationToken cancellationToken)
        {
            return await _authService.RefreshToken(refreshToken, cancellationToken);
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
