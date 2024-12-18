using Eshop.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.IocConfig
{
    public static class AddDynamicPermissionExtensions
    {
        public static IServiceCollection AddDynamicPermission(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
            services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
            services.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();

            return services;
        }
    }
}
