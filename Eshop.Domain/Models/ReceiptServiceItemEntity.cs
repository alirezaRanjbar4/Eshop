using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class ReceiptServiceItemEntity : BaseTrackedModel, IBaseEntity
    {
        public string? Description { get; set; }
        public long Count { get; set; }
        public long Price { get; set; }
        public long? DiscountPrice { get; set; }
        public int? DiscountPercent { get; set; }
        public int? ValueAddedPercent { get; set; }

        public Guid ReceiptId { get; set; }
        public virtual ReceiptEntity Receipt { get; set; }

        public Guid ServiceId { get; set; }
        public virtual ServiceEntity Service { get; set; }
    }
}