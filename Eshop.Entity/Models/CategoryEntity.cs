using Eshop.Entity.General;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class CategoryEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public CategoryType Type { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        public Guid? ParentId { get; set; }
        public virtual CategoryEntity? Parent { get; set; }

        public virtual ICollection<CategoryEntity> Childs { get; set; }
        public virtual ICollection<ProductCategoryEntity> ProductCategories { get; set; }
        public virtual ICollection<ServiceCategoryEntity> ServiceCategories { get; set; }
    }
}