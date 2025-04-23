using Eshop.Domain.Identities;
using Eshop.Infrastructure.DBContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.IocConfig
{
    public static class AddIdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServiceConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<UserEntity, RoleEntity>()
                    .AddEntityFrameworkStores<ApplicationContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
