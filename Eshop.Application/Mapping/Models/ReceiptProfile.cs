using AutoMapper;
using Eshop.Application.DTO.Models.Receipt;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptEntity, ReceiptDTO>().ReverseMap();

            CreateMap<AddReceiptDTO, ReceiptEntity>();

            CreateMap<AddReceiptDTO, ReceiptDTO>();

            CreateMap<ReceiptProductItemEntity, ReceiptProductItemDTO>().ReverseMap();

            CreateMap<ReceiptServiceItemEntity, ReceiptServiceItemDTO>().ReverseMap();
        }
    }
}
