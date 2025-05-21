using Eshop.Domain.General;
using Eshop.Share.Enum;

namespace Eshop.Domain.Models
{
    public class AccountPartyEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Description { get; set; }
        public AccountPartyType Type { get; set; }
        public long CurrentCredit { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<ReceiptEntity> Receipts { get; set; }
        public virtual ICollection<FinancialDocumentEntity> FinancialDocuments { get; set; }
    }
}