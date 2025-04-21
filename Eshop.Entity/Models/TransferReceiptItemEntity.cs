using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class TransferReceiptItemEntity : BaseTrackedModel, IBaseEntity
    {
        public string? Description { get; set; }
        public float Count { get; set; }

        public Guid TransferReceiptId { get; set; }
        public virtual TransferReceiptEntity TransferReceipt { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        public Guid EnteredWarehouseLocationId { get; set; }
        public virtual WarehouseLocationEntity EnteredWarehouseLocation { get; set; }

        public Guid ExitedWarehouseLocationId { get; set; }
        public virtual WarehouseLocationEntity ExitedWarehouseLocation { get; set; }
    }
}