using Eshop.Domain.General;
using Eshop.Share.Enum;

namespace Eshop.Domain.Models
{
    public class ReceiptEntity : BaseTrackedModel, IBaseEntity
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public int ReceiptNumber { get; set; }
        public string? ReceiptSerial { get; set; }
        public bool IsFinalized { get; set; }
        public ReceiptType Type { get; set; }

        public Guid AccountPartyId { get; set; }
        public virtual AccountPartyEntity AccountParty { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<ReceiptProductItemEntity>? ProductItems { get; set; }
        public virtual ICollection<ReceiptServiceItemEntity>? ServiceItems { get; set; }
        public virtual ICollection<ReceiptFinancialDocumentEntity>? ReceiptFinancialDocuments { get; set; }
    }
}