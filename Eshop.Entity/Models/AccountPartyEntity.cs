using Eshop.Entity.General;
using Eshop.Entity.Identities;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class AccountPartyEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public AccountPartyType Type { get; set; }

        //public Guid UserId { get; set; }
        //public virtual UserEntity User { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<ShoppingCardItemEntity> ShoppingCardItems { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<ReceiptEntity> Receipts { get; set; }
    }
}