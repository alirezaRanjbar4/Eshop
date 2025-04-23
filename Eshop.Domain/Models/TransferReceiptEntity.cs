using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class TransferReceiptEntity : BaseTrackedModel, IBaseEntity
    {
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsFinalized { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<TransferReceiptItemEntity> Items { get; set; }
    }
}