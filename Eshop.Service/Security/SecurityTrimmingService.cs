using Eshop.Common.Exceptions;
using Eshop.DTO.Identities.DynamicAccess;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace Eshop.Service.Security
{
    public class SecurityTrimmingService : ISecurityTrimmingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;

        public SecurityTrimmingService(
            IHttpContextAccessor httpContextAccessor,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
            _httpContextAccessor = httpContextAccessor;
            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
        }

        public bool CanCurrentUserAccess(string area, string controller, string action)
        {
            return _httpContextAccessor.HttpContext != null && CanUserAccess(_httpContextAccessor.HttpContext.User, area, controller, action);
        }

        public bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            var currentClaimValue = $"{area}:{controller}:{action}";
            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            if (!securedControllerActions.SelectMany(x => x.MvcActions).Any(x => x.ActionId == currentClaimValue))
            {
                throw new KeyNotFoundException($@"The `secured` area={area}/controller={controller}/action={action} with `ConstantPolicies.DynamicPermission` policy not found. Please check you have entered the area/controller/action names correctly and also it's decorated with the correct security policy.");
            }


            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            var result = user.HasClaim(claim => claim.Type == ConstantPolicies.DynamicPermissionClaimType &&
                                          claim.Value == currentClaimValue);
            if (!result)
            {
                throw new AppException(HttpStatusCode.Unauthorized, "You are unauthorized to access this resource.", HttpStatusCode.Forbidden);
            }

            return result;
        }

    }
}
