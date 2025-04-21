using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Repository.General;
using Eshop.Service.General;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.IocConfig;

public static class IocContainer
{
    public static void Injector(this IServiceCollection services)
    {
        services.AddInjection<IScopedDependency>();
        services.AddInjection<ITransientDependency>();
        services.AddInjection<ISingletonDependency>();
    }

    public static void AddInjection<T>(this IServiceCollection services)
    {
        var assemblyService = typeof(IBaseService<>).Assembly;
        var assemblyRepository = typeof(IBaseRepository<>).Assembly;

        IEnumerable<Type?> typesService = assemblyService.GetTypes().Where(x => x.IsClass || x.IsInterface);
        IEnumerable<Type?> typesRepository = assemblyRepository.GetTypes().Where(x => x.IsClass || x.IsInterface);

        List<Type?> typesQuery = new List<Type?>();

        typesQuery.AddRange(typesRepository);
        typesQuery.AddRange(typesService);

        foreach (Type? type in typesQuery)
        {
            if (type.IsInterface && !type.IsGenericType && typeof(T).IsAssignableFrom(type) && type.Name != nameof(T))
            {
                var interfaceObject = type;
                var classObject = typesQuery.Where(x => x.IsClass && type.IsAssignableFrom(x));
                if (classObject.Count() > 1)
                {
                    throw new Exception("every class should implemented one interface");
                }
                if (typeof(T).Name == nameof(IScopedDependency))
                {
                    services.AddScoped(interfaceObject, classObject.SingleOrDefault()!);
                }
                else if (typeof(T).Name == nameof(ISingletonDependency))
                {
                    services.AddSingleton(interfaceObject, classObject.SingleOrDefault());
                }
                else
                    services.AddTransient(interfaceObject, classObject.SingleOrDefault());

            }
        }
    }
}