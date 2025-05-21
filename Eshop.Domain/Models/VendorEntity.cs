using Eshop.Domain.General;
using Eshop.Domain.Identities;

namespace Eshop.Domain.Models
{
    public class VendorEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public virtual UserEntity User { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<SchedulerTaskVendorEntity> SchedulerTaskVendors { get; set; }
    }
}