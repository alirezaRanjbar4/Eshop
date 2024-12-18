using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class OrderItemEntity : BaseTrackedModel, IBaseEntity
    {
        public int RequestedAmount { get; set; }
        public int FinalAmount { get; set; }
        public long PrimaryPrice { get; set; }
        public long FinalPrice { get; set; }

        public Guid OrderId { get; set; }
        public virtual OrderEntity Order { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}