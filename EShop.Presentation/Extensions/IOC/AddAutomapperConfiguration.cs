using AutoMapper;
using Eshop.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eshop.Presentation.Extensions.IOC
{
    public static class AddAutomapperConfiguration
    {
        public static void AddAutomapper(this IServiceCollection services)
        {
            Assembly assembly = typeof(MappingProfile).Assembly;
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(assembly);
            });
            var mapper = configuration.CreateMapper();
            services.AddAutoMapper(assembly);
        }
    }
}
