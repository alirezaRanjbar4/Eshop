using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ServiceEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<ServiceCategoryEntity> ServiceCategories { get; set; }
        public virtual ICollection<ImageEntity> Images { get; set; }
        public virtual ICollection<ServicePriceEntity> ServicePrices { get; set; }

    }
}