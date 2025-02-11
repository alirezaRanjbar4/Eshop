using AutoMapper;
using Eshop.Common.Exceptions;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ProductTransferProfile : Profile
    {
        public ProductTransferProfile()
        {
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
