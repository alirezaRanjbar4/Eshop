using AutoMapper;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ProductWarehouseLocationProfile : Profile
    {
        public ProductWarehouseLocationProfile()
        {
            CreateMap<ProductWarehouseLocationEntity, ProductWarehouseLocationDTO>().ReverseMap();

            CreateMap<ProductWarehouseLocationEntity, GetAllProductWarehouseLocationDTO>()
                   .ForMember(des => des.WarehouseLocation, option => option.MapFrom(src => src.WarehouseLocation != null ? src.WarehouseLocation.Name : string.Empty))
                   .ForMember(des => des.Warehouse, option => option.MapFrom(src => src.WarehouseLocation != null && src.WarehouseLocation.Warehouse != null ? src.WarehouseLocation.Warehouse.Name : string.Empty))
                   .ForMember(des => des.Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));
        }
    }
}
