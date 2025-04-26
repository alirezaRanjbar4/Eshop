using Eshop.Application.Service.FileStorage;
using Eshop.Application.Service.General;
using Eshop.Application.Service.Identity.Authentication;
using Eshop.Application.Service.Identity.JWT;
using Eshop.Application.Service.Identity.Role;
using Eshop.Application.Service.Identity.User;
using Eshop.Application.Service.Models.FinancialDocument;
using Eshop.Application.Service.Models.Product;
using Eshop.Application.Service.Models.Receipt;
using Eshop.Application.Service.Models.Service;
using Eshop.Application.Service.Models.TransferReceipt;
using Eshop.Application.Service.Models.Vendor;
using Eshop.Application.Service.Models.Warehouse;
using Eshop.Application.Service.Security;
using Eshop.Domain.Identities;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Eshop.Infrastructure.Repository.Identities.User;
using Eshop.Infrastructure.Repository.Models.AccountParty;
using Eshop.Presentation.Components;
using Eshop.Share.ActionFilters;
using Eshop.Share.Helpers.Utilities.Interface;
using Eshop.Share.Helpers.Utilities.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Presentation.Extensions.IOC;

public static class AddDependencyInjectionConfiguration
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddInjection<IScopedDependency>();
        services.AddInjection<ITransientDependency>();
        services.AddInjection<ISingletonDependency>();
    }

    public static void AddInjection<T>(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService, FileStorageService>();
        services.AddScoped<UserActionFilter>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddScoped<IUtility, Utility>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IJWTService, JWTService>();
        services.AddSingleton<IAuthorizationHandler, DynamicPermissionsAuthorizationHandler>();
        services.AddSingleton<IMvcActionsDiscoveryService, MvcActionsDiscoveryService>();
        services.AddSingleton<ISecurityTrimmingService, SecurityTrimmingService>();


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountPartyRepository, AccountPartyRepository>();
        services.AddScoped<IBaseRepository<RoleEntity>, BaseRepository<RoleEntity>>();
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
        services.AddScoped<IBaseRepository<RefreshTokenEntity>, BaseRepository<RefreshTokenEntity>>();




        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFinancialDocumentService, FinancialDocumentService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<ITransferReceiptService, TransferReceiptService>();
        services.AddScoped<IVendorService, VendorService>();
        services.AddScoped<IWarehouseService, WarehouseService>();
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


    }
}