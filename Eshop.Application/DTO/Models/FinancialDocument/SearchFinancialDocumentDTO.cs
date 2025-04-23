using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.FinancialDocument
{
    public class SearchFinancialDocumentDTO : BaseSearchDTO
    {
        public FinancialDocumentType? Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? AccountPartyId { get; set; }
    }
}