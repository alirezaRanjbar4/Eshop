using Eshop.Application.Service.General;
using Eshop.Domain.Identities;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Eshop.Share.Helpers.Utilities.Interface;
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
        services.AddScoped<IBaseRepository<RoleEntity>, BaseRepository<RoleEntity>>();
        services.AddScoped<IBaseRepository<UserRoleEntity>, BaseRepository<UserRoleEntity>>();
        services.AddScoped<IBaseRepository<AccountPartyEntity>, BaseRepository<AccountPartyEntity>>();
        services.AddScoped<IBaseRepository<CategoryEntity>, BaseRepository<CategoryEntity>>();
        services.AddScoped<IBaseRepository<DemoRequestEntity>, BaseRepository<DemoRequestEntity>>();
        services.AddScoped<IBaseRepository<FinancialDocumentEntity>, BaseRepository<FinancialDocumentEntity>>();
        services.AddScoped<IBaseRepository<ImageEntity>, BaseRepository<ImageEntity>>();
        services.AddScoped<IBaseRepository<ProductCategoryEntity>, BaseRepository<ProductCategoryEntity>>();
        services.AddScoped<IBaseRepository<ProductEntity>, BaseRepository<ProductEntity>>();
        services.AddScoped<IBaseRepository<ProductPriceEntity>, BaseRepository<ProductPriceEntity>>();
        services.AddScoped<IBaseRepository<ProductTransferEntity>, BaseRepository<ProductTransferEntity>>();
        services.AddScoped<IBaseRepository<ProductWarehouseLocationEntity>, BaseRepository<ProductWarehouseLocationEntity>>();
        services.AddScoped<IBaseRepository<ReceiptEntity>, BaseRepository<ReceiptEntity>>();
        services.AddScoped<IBaseRepository<ReceiptFinancialDocumentEntity>, BaseRepository<ReceiptFinancialDocumentEntity>>();
        services.AddScoped<IBaseRepository<ReceiptProductItemEntity>, BaseRepository<ReceiptProductItemEntity>>();
        services.AddScoped<IBaseRepository<ReceiptServiceItemEntity>, BaseRepository<ReceiptServiceItemEntity>>();
        services.AddScoped<IBaseRepository<ServiceCategoryEntity>, BaseRepository<ServiceCategoryEntity>>();
        services.AddScoped<IBaseRepository<ServiceEntity>, BaseRepository<ServiceEntity>>();
        services.AddScoped<IBaseRepository<ServicePriceEntity>, BaseRepository<ServicePriceEntity>>();
        services.AddScoped<IBaseRepository<StoreEntity>, BaseRepository<StoreEntity>>();
        services.AddScoped<IBaseRepository<StorePaymentEntity>, BaseRepository<StorePaymentEntity>>();
        services.AddScoped<IBaseRepository<TransferReceiptEntity>, BaseRepository<TransferReceiptEntity>>();
        services.AddScoped<IBaseRepository<TransferReceiptItemEntity>, BaseRepository<TransferReceiptItemEntity>>();
        services.AddScoped<IBaseRepository<VendorEntity>, BaseRepository<VendorEntity>>();
        services.AddScoped<IBaseRepository<WarehouseEntity>, BaseRepository<WarehouseEntity>>();
        services.AddScoped<IBaseRepository<WarehouseLocationEntity>, BaseRepository<WarehouseLocationEntity>>();



        services.AddScoped<IBaseService<RoleEntity>, BaseService<RoleEntity>>();
        services.AddScoped<IBaseService<UserRoleEntity>, BaseService<UserRoleEntity>>();
        services.AddScoped<IBaseService<AccountPartyEntity>, BaseService<AccountPartyEntity>>();
        services.AddScoped<IBaseService<CategoryEntity>, BaseService<CategoryEntity>>();
        services.AddScoped<IBaseService<DemoRequestEntity>, BaseService<DemoRequestEntity>>();
        services.AddScoped<IBaseService<FinancialDocumentEntity>, BaseService<FinancialDocumentEntity>>();
        services.AddScoped<IBaseService<ImageEntity>, BaseService<ImageEntity>>();
        services.AddScoped<IBaseService<ProductCategoryEntity>, BaseService<ProductCategoryEntity>>();
        services.AddScoped<IBaseService<ProductEntity>, BaseService<ProductEntity>>();
        services.AddScoped<IBaseService<ProductPriceEntity>, BaseService<ProductPriceEntity>>();
        services.AddScoped<IBaseService<ProductTransferEntity>, BaseService<ProductTransferEntity>>();
        services.AddScoped<IBaseService<ProductWarehouseLocationEntity>, BaseService<ProductWarehouseLocationEntity>>();
        services.AddScoped<IBaseService<ReceiptEntity>, BaseService<ReceiptEntity>>();
        services.AddScoped<IBaseService<ReceiptFinancialDocumentEntity>, BaseService<ReceiptFinancialDocumentEntity>>();
        services.AddScoped<IBaseService<ReceiptProductItemEntity>, BaseService<ReceiptProductItemEntity>>();
        services.AddScoped<IBaseService<ReceiptServiceItemEntity>, BaseService<ReceiptServiceItemEntity>>();
        services.AddScoped<IBaseService<ServiceCategoryEntity>, BaseService<ServiceCategoryEntity>>();
        services.AddScoped<IBaseService<ServiceEntity>, BaseService<ServiceEntity>>();
        services.AddScoped<IBaseService<ServicePriceEntity>, BaseService<ServicePriceEntity>>();
        services.AddScoped<IBaseService<StoreEntity>, BaseService<StoreEntity>>();
        services.AddScoped<IBaseService<StorePaymentEntity>, BaseService<StorePaymentEntity>>();
        services.AddScoped<IBaseService<TransferReceiptEntity>, BaseService<TransferReceiptEntity>>();
        services.AddScoped<IBaseService<TransferReceiptItemEntity>, BaseService<TransferReceiptItemEntity>>();
        services.AddScoped<IBaseService<VendorEntity>, BaseService<VendorEntity>>();
        services.AddScoped<IBaseService<WarehouseEntity>, BaseService<WarehouseEntity>>();
        services.AddScoped<IBaseService<WarehouseLocationEntity>, BaseService<WarehouseLocationEntity>>();


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