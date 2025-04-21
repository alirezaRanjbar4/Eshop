using AutoMapper;
using Eshop.Common.Exceptions;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Models.FinancialDocument;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class FinancialDocumentProfile : Profile
    {
        public FinancialDocumentProfile()
        {
            CreateMap<FinancialDocumentEntity, FinancialDocumentDTO>().ReverseMap();

            CreateMap<AddFinancialDocumentDTO, FinancialDocumentEntity>();

            CreateMap<FinancialDocumentEntity, GetFinancialDocumentDTO>()
                 .ForMember(des => des.String_Type, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                 .ForMember(des => des.String_PaymentMethod, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                 .ForMember(des => des.String_Date, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)));

            CreateMap<ReceiptFinancialDocumentEntity, ReceiptFinancialDocumentDTO>().ReverseMap();
        }
    }
}
