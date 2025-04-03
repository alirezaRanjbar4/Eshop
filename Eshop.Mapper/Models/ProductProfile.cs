using AutoMapper;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;
using Eshop.Common.Enum;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.Common.Exceptions;

namespace Eshop.Mapper.Models
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductDTO>()
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ProductPrices != null && src.ProductPrices.Any(x => x.ExpiryDate == null) ? src.ProductPrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
                .ForMember(des => des.ProductCategoryIds, option => option.MapFrom(src => src.ProductCategories != null ? src.ProductCategories.Select(x => x.CategoryId) : null))
                .ReverseMap();

            CreateMap<ProductEntity, GetProductDTO>()
               .ForMember(des => des.Price, option => option.MapFrom(src => src.ProductPrices != null && src.ProductPrices.Any(x => x.ExpiryDate == null) ? src.ProductPrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
               .ReverseMap();

            CreateMap<ProductEntity, GetAllProductDTO>()
                .ForMember(des => des.Category, option => option.MapFrom(src => src.ProductCategories != null && src.ProductCategories.Any() ? string.Join("،", src.ProductCategories.Select(x => x.Category.Name)) : string.Empty))
                .ForMember(des => des.TotalCount, option => option.MapFrom(src => src.ProductWarehouseLocations != null && src.ProductWarehouseLocations.Any() ? src.ProductWarehouseLocations.Sum(x => x.Count) : 0))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ProductPrices != null && src.ProductPrices.Any(x => x.ExpiryDate == null) ? src.ProductPrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
                .ForMember(des => des.MeasurementUnit, option => option.MapFrom(src => src.MeasurementUnit.GetDescription()));

            CreateMap<ImageEntity, ImageDTO>().ReverseMap();


            CreateMap<ProductPriceEntity, ProductPriceDTO>().ReverseMap();

            CreateMap<ProductPriceEntity, CompleteProductPriceDTO>()
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => src.ExpiryDate.HasValue ? Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate.Value, false) : string.Empty))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));


            CreateMap<ProductTransferEntity, ProductTransferDTO>().ReverseMap();

            CreateMap<ProductTransferEntity, CompleteProductTransferDTO>()
                .ForMember(des => des.Type, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                .ForMember(des => des.Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .ForMember(des => des.WarehouseLocation, option => option.MapFrom(src => src.WarehouseLocation != null ? src.WarehouseLocation.Name : string.Empty))
                .ForMember(des => des.Warehouse, option => option.MapFrom(src => src.WarehouseLocation.Warehouse != null ? src.WarehouseLocation.Warehouse.Name : string.Empty))
                .ForMember(des => des.Date, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, true)));
        }
    }
}
