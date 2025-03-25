using AutoMapper;
using Eshop.DTO.Models.Receipt;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptEntity, ReceiptDTO>().ReverseMap();

            CreateMap<AddReceiptDTO, ReceiptEntity>();

            CreateMap<AddReceiptDTO, ReceiptDTO>();

            CreateMap<ReceiptItemEntity, ReceiptItemDTO>().ReverseMap();
        }
    }
}
