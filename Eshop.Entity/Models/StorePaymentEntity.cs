using Eshop.Entity.General;
using Eshop.Common.Enum;

namespace Eshop.Entity.Models
{
    public class StorePaymentEntity : BaseTrackedModel, IBaseEntity
    {
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }
        public FinancialDocumentPaymentMethod PaymentMethod { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }
    }
}