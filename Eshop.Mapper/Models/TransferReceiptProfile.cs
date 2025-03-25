using AutoMapper;
using Eshop.DTO.Models.TransferReceipt;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class TransferReceiptProfile : Profile
    {
        public TransferReceiptProfile()
        {
            CreateMap<TransferReceiptEntity, TransferReceiptDTO>().ReverseMap();

            CreateMap<TransferReceiptItemEntity, TransferReceiptItemDTO>().ReverseMap();
        }
    }
}
