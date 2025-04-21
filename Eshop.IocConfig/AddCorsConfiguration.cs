using Microsoft.Extensions.DependencyInjection;

namespace Eshop.IocConfig
{
    public static class AddCorsConfiguration
    {
        public static IServiceCollection AddCorss(this IServiceCollection services)
        {
            services.AddCors(options =>
                             options.AddPolicy("CorsPolicy", policy =>
                             {
                                 policy.AllowAnyHeader()
                                       .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                                       .WithOrigins("http://192.168.247.233",
                                                    "http://192.168.43.154",
                                                    "http://192.168.43.244",
                                                    "http://192.168.4.143:3000",
                                                    "http://192.168.5.134:3000",
                                                    "http://localhost:3000",
                                                    "http://192.168.5.129",
                                                    "http://192.168.43.154:3000")
                                       .AllowCredentials();
                             }));

            return services;
        }
    }
}
