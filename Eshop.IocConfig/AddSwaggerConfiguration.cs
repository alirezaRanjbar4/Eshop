using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Eshop.IocConfig
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
                            //Url = new Uri("arranjbar2@gmail.com"),
                        },
                    });

                var filePath = Path.Combine(AppContext.BaseDirectory, "RainstormTech.API.xml");
                c.IncludeXmlComments(filePath);
            });

            return services;
        }
    }
}
