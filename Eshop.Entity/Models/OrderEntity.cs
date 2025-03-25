using Eshop.Entity.General;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class OrderEntity : BaseTrackedModel, IBaseEntity
    {
        public OrderStatus Status { get; set; }

        public Guid AccountPartyId { get; set; }
        public virtual AccountPartyEntity AccountParty { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}