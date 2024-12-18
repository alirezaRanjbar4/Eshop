using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class WarehouseEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLock { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<WarehouseLocationEntity>? WarehouseLocations { get; set; }
    }
}