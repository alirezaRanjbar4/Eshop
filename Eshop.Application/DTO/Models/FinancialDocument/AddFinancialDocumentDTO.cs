using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.FinancialDocument
{
    public class AddFinancialDocumentDTO : BaseDTO
    {
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public FinancialDocumentType Type { get; set; }
        public FinancialDocumentPaymentMethod PaymentMethod { get; set; }

        public Guid StoreId { get; set; }
        public Guid? AccountPartyId { get; set; }

        public List<Guid> ReceiptIds { get; set; }
    }
}