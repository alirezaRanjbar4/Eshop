using AutoMapper;
using Eshop.Application.DTO.Models.TransferReceipt;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
{
    public class TransferReceiptProfile : Profile
    {
        public TransferReceiptProfile()
        {
            CreateMap<TransferReceiptEntity, TransferReceiptDTO>().ReverseMap();

            CreateMap<AddTransferReceiptDTO, TransferReceiptEntity>();

            CreateMap<AddTransferReceiptDTO, TransferReceiptDTO>();

            CreateMap<TransferReceiptItemEntity, TransferReceiptItemDTO>().ReverseMap();
        }
    }
}
