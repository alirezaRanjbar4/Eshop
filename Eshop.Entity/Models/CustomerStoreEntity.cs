using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class CustomerStoreEntity : BaseTrackedModel, IBaseEntity
    {
        public Guid CustomerId { get; set; }
        public virtual CustomerEntity Customer { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }
    }
}