using AutoMapper;
using Eshop.Application.DTO.Models.Receipt;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Utilities;

namespace Eshop.Application.Mapping.Models
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptEntity, ReceiptDTO>().ReverseMap();

            CreateMap<ReceiptEntity, SimpleReceiptDTO>()
                .ForMember(des => des.AccountParty, option => option.MapFrom(src => src.AccountParty != null ? src.AccountParty.Name : string.Empty));

            CreateMap<ReceiptEntity, GetAllReceiptDTO>()
                .ForMember(des => des.String_AccountParty, option => option.MapFrom(src => src.AccountParty != null ? src.AccountParty.Name : string.Empty))
                .ForMember(des => des.String_Date, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)))
                .ForMember(des => des.String_TotalAmount, option => option.MapFrom(src => "0"));

            CreateMap<AddReceiptDTO, ReceiptEntity>();

            CreateMap<AddReceiptDTO, ReceiptDTO>();

            CreateMap<ReceiptProductItemEntity, ReceiptProductItemDTO>().ReverseMap();

            CreateMap<ReceiptProductItemEntity, GetReceiptProductItemDTO>()
                .ForMember(des => des.String_Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty));

            CreateMap<ReceiptServiceItemEntity, ReceiptServiceItemDTO>().ReverseMap();

            CreateMap<ReceiptServiceItemEntity, GetReceiptServiceItemDTO>()
                .ForMember(des => des.String_Service, option => option.MapFrom(src => src.Service != null ? src.Service.Name : string.Empty));
        }
    }
}
