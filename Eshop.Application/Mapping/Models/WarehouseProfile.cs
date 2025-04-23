using AutoMapper;
using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Models.Product;
using Eshop.Application.DTO.Models.Warehouse;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<WarehouseEntity, WarehouseDTO>().ReverseMap();

            CreateMap<AddWarehouseDTO, WarehouseEntity>().ReverseMap();

            CreateMap<AddWarehouseDTO, WarehouseDTO>();

            CreateMap<WarehouseLocationEntity, WarehouseLocationDTO>().ReverseMap();

            CreateMap<ProductWarehouseLocationEntity, ProductWarehouseLocationDTO>().ReverseMap();

            CreateMap<ProductWarehouseLocationEntity, SimpleDTO>()
                .ForMember(des => des.Key, option => option.MapFrom(src => src.WarehouseLocation != null && src.WarehouseLocation.Warehouse != null ? $"{src.WarehouseLocation.Name}/{src.WarehouseLocation.Warehouse.Name}/{src.Count}" : string.Empty))
                .ForMember(des => des.Value, option => option.MapFrom(src => src.Id));

            CreateMap<ProductWarehouseLocationEntity, GetAllProductWarehouseLocationDTO>()
                   .ForMember(des => des.WarehouseLocation, option => option.MapFrom(src => src.WarehouseLocation != null ? src.WarehouseLocation.Name : string.Empty))
                   .ForMember(des => des.Warehouse, option => option.MapFrom(src => src.WarehouseLocation != null && src.WarehouseLocation.Warehouse != null ? src.WarehouseLocation.Warehouse.Name : string.Empty))
                   .ForMember(des => des.Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));

            CreateMap<ProductWarehouseLocationEntity, WarehouseInventoryDTO>()
                   .ForMember(des => des.WarehouseLocation, option => option.MapFrom(src => src.WarehouseLocation != null ? src.WarehouseLocation.Name : string.Empty))
                   .ForMember(des => des.Count, option => option.MapFrom(src => src.Count))
                   .ForMember(des => des.Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));
        }
    }
}
