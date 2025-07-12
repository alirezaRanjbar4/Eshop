using AutoMapper;
using Eshop.Application.DTO.Models.FinancialDocument;
using Eshop.Domain.Models;
using Eshop.Share.Exceptions;
using Eshop.Share.Helpers.Utilities.Utilities;

namespace Eshop.Application.Mapping.Models
{
    public class FinancialDocumentProfile : Profile
    {
        public FinancialDocumentProfile()
        {
            CreateMap<FinancialDocumentDTO, FinancialDocumentEntity>()
                .ReverseMap()
                .ForMember(des => des.ReceiptIds, option => option.MapFrom(src => src.ReceiptFinancialDocuments != null && src.ReceiptFinancialDocuments.Any() ? src.ReceiptFinancialDocuments.Select(x => x.ReceiptId) : null));

            CreateMap<FinancialDocumentEntity, GetFinancialDocumentDTO>()
                 .ForMember(des => des.String_Type, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                 .ForMember(des => des.String_PaymentMethod, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                 .ForMember(des => des.String_Date, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)));

            CreateMap<ReceiptFinancialDocumentEntity, ReceiptFinancialDocumentDTO>().ReverseMap();
        }
    }
}
