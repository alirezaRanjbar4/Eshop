using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.FinancialDocument
{
    public class FinancialDocumentDTO : BaseDTO
    {
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public FinancialDocumentType Type { get; set; }
        public FinancialDocumentPaymentMethod PaymentMethod { get; set; }

        public Guid StoreId { get; set; }
        public Guid? AccountPartyId { get; set; }
    }
}