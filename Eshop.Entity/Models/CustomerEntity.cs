using Eshop.Entity.General;
using Eshop.Entity.Identities;

namespace Eshop.Entity.Models
{
    public class CustomerEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public virtual ICollection<CustomerStoreEntity> CustomerStores { get; set; }
        public virtual ICollection<ShoppingCardItemEntity> ShoppingCardItems { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
    }
}