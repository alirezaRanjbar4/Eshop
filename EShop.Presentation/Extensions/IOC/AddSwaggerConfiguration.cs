using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Eshop.Presentation.Extensions.IOC
{
    public static class AddSwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Sweet System API",
                        Version = "v1",
                        Description = "API for handling user/content data",
                        Contact = new OpenApiContact
                        {
                            Name = "Alireza",
                            Email = "arranjbar2@gmail.com",
                        },
                    });
            });

            return services;
        }
    }
}
