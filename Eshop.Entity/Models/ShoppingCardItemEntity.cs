using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ShoppingCardItemEntity : BaseTrackedModel, IBaseEntity
    {
        public int Count { get; set; }

        public Guid AccountPartyId { get; set; }
        public virtual AccountPartyEntity AccountParty { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}