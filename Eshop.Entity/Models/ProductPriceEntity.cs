using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ProductPriceEntity : BaseTrackedModel, IBaseEntity
    {
        public long Price { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Guid? ProductId { get; set; }
        public virtual ProductEntity? Product { get; set; }
    }
}