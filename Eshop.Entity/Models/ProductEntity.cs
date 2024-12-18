using Eshop.Entity.General;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class ProductEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public bool OpenToSell { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public virtual ICollection<ProductCategoryEntity> ProductCategories { get; set; }
        public virtual ICollection<ProductWarehouseLocationEntity> ProductWarehouseLocations { get; set; }
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
        public virtual ICollection<ProductImageEntity> ProductImages { get; set; }
        public virtual ICollection<ShoppingCardItemEntity> ShoppingCardItems { get; set; }

    }
}