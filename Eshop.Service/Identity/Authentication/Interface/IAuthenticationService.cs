using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Identities.Authentication;
using Eshop.DTO.Identities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Service.Identity.Authentication.Interface
{
    public interface IAuthenticationService : IScopedDependency
    {
        Task<LoginDTO> Login(Login model, HttpContext httpContext, CancellationToken cancellationToken);
        Task<OperationResult<string>> Logout(HttpContext httpContext);
        Task<IdentityResult> ResetPassword(ResetPassword resetPassword);
    }
}
