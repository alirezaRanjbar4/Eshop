using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.FinancialDocument
{
    public class GetFinancialDocumentDTO : BaseDto
    {
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public string? String_Date { get; set; }
        public FinancialDocumentType Type { get; set; }
        public string? String_Type { get; set; }
        public FinancialDocumentPaymentMethod PaymentMethod { get; set; }
        public string? String_PaymentMethod { get; set; }

        public Guid StoreId { get; set; }
        public Guid? AccountPartyId { get; set; }
        public string? String_AccountPartyId { get; set; }
    }
}