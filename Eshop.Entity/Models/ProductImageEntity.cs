using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ProductImageEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}