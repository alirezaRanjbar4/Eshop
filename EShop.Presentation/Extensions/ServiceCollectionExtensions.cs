using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Presentation.Extensions.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Eshop.Presentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AppSetting(configuration);

            services.AddHttpContextAccessor();

            services.AddSwagger();

            services.AddDbContext(configuration);

            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddIdentityServiceConfiguration();

            services.AddCorsPolicy(configuration);

            services.AddDependencyInjection();

            services.AddAutomapper();

            services.AddCustomAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ConstantPolicies.DynamicPermission, policy =>
                    policy.Requirements.Add(new DynamicPermissionRequirement()));
            });

            services.AddLocalization();

            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "AntiforgeryField";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.Configure<FormOptions>(options =>
            {
                options.MultipartBodyLengthLimit = 404857600;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = 404857600;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = 404857600;
            });

            return services;
        }
    }
}
