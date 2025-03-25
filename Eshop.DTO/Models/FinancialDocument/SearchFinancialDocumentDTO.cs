using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.FinancialDocument
{
    public class SearchFinancialDocumentDTO : BaseSearchDTO
    {
        public Guid StoreId { get; set; }
        public FinancialDocumentType? Type { get; set; }
    }
}