using AutoMapper;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;
using Eshop.Enum;

namespace Eshop.Mapper.Models
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductEntity, ProductDTO>()
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ProductPrices != null ? src.ProductPrices.Last().Price : 0))
                .ForMember(des => des.ProductCategoryIds, option => option.MapFrom(src => src.ProductCategories != null ? src.ProductCategories.Select(x => x.CategoryId) : null))
                .ReverseMap();

            CreateMap<ProductEntity, GetProductDTO>()
               .ForMember(des => des.Price, option => option.MapFrom(src => src.ProductPrices != null ? src.ProductPrices.Last().Price : 0))
               .ReverseMap();

            CreateMap<ProductEntity, GetAllProductDTO>()
                .ForMember(des => des.Category, option => option.MapFrom(src => src.ProductCategories != null ? src.ProductCategories.Select(x => x.Category.Name).ToString() : string.Empty))
                .ForMember(des => des.TotalCount, option => option.MapFrom(src => src.ProductWarehouseLocations.Sum(x => x.Count)))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ProductPrices != null ? src.ProductPrices.Last().Price : 0))
                .ForMember(des => des.MeasurementUnit, option => option.MapFrom(src => src.MeasurementUnit.GetDescription()));
        }
    }
}
