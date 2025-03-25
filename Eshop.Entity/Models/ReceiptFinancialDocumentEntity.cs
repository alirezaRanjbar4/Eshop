using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ReceiptFinancialDocumentEntity : BaseTrackedModel, IBaseEntity
    {
        public Guid ReceiptId { get; set; }
        public virtual ReceiptEntity Receipt { get; set; }

        public Guid FinancialDocumentId { get; set; }
        public virtual FinancialDocumentEntity FinancialDocument { get; set; }
    }
}