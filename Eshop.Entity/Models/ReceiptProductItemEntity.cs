using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ReceiptProductItemEntity : BaseTrackedModel, IBaseEntity
    {
        public string? Description { get; set; }
        public float Count { get; set; }
        public long Price { get; set; }
        public long? DiscountPrice { get; set; }
        public int? DiscountPercent { get; set; }
        public int? ValueAdded { get; set; }

        public Guid ReceiptId { get; set; }
        public virtual ReceiptEntity Receipt { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        public Guid WarehouseLocationId { get; set; }
        public virtual WarehouseLocationEntity WarehouseLocation { get; set; }
    }
}