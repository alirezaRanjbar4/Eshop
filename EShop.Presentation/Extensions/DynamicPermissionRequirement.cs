using Eshop.Application.Service.General.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Threading.Tasks;

namespace Eshop.Presentation.Extensions
{
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService, IHttpContextAccessor httpContextAccessor)
        {
            _securityTrimmingService = securityTrimmingService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var actionDescriptor = httpContext.GetEndpoint()?.Metadata.GetMetadata<ControllerActionDescriptor>();

            if (actionDescriptor == null)
            {
                return Task.CompletedTask;
            }

            actionDescriptor.RouteValues.TryGetValue("area", out var areaName);
            var area = string.IsNullOrWhiteSpace(areaName) ? string.Empty : areaName;

            actionDescriptor.RouteValues.TryGetValue("controller", out var controllerName);
            var controller = string.IsNullOrWhiteSpace(controllerName) ? string.Empty : controllerName;

            actionDescriptor.RouteValues.TryGetValue("action", out var actionName);
            var action = string.IsNullOrWhiteSpace(actionName) ? string.Empty : actionName;

            if (_securityTrimmingService.CanCurrentUserAccess(area, controller, action))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
