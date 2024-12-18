using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ProductCategoryEntity : BaseTrackedModel, IBaseEntity
    {
        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}