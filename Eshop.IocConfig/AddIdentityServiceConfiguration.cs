using Eshop.Entity.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Rasam.Data.DBContext;

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
