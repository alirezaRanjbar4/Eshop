using AutoMapper;
using Eshop.DTO.Models.FinancialDocument;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class FinancialDocumentProfile : Profile
    {
        public FinancialDocumentProfile()
        {
            CreateMap<FinancialDocumentEntity, FinancialDocumentDTO>().ReverseMap();

            CreateMap<ReceiptFinancialDocumentEntity, ReceiptFinancialDocumentDTO>().ReverseMap();
        }
    }
}
