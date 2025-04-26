using Eshop.Application.DTO.Identities.Authentication;
using Eshop.Application.DTO.Identities.User;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Helpers.Utilities.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Application.Service.Identity.Authentication
{
    public interface IAuthenticationService : IScopedDependency
    {
        Task<LoginDTO> Login(Login model, HttpContext httpContext, CancellationToken cancellationToken);
        Task<LoginDTO?> RefreshToken(TokenRequestDTO request, CancellationToken cancellationToken);
        Task<OperationResult<string>> Logout(HttpContext httpContext);
        Task<IdentityResult> ResetPassword(ResetPassword resetPassword);
    }
}
