using Eshop.Entity.General;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class FinancialDocumentEntity : BaseTrackedModel, IBaseEntity
    {
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public FinancialDocumentType Type { get; set; }
        public FinancialDocumentPaymentMethod PaymentMethod { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public Guid? AccountPartyId { get; set; }
        public virtual AccountPartyEntity? AccountParty { get; set; }

        public virtual ICollection<ReceiptFinancialDocumentEntity>? ReceiptFinancialDocuments { get; set; }
    }
}