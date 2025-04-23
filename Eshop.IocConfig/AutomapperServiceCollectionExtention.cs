using AutoMapper;
using Eshop.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Eshop.IocConfig
{
    public static class AutomapperServiceCollectionExtention
    {
        public static void AddAutomapperServiceConfiguration(this IServiceCollection services)
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
