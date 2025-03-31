using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.FinancialDocument
{
    public class SearchFinancialDocumentDTO : BaseSearchDTO
    {
        public FinancialDocumentType? Type { get; set; }
    }
}