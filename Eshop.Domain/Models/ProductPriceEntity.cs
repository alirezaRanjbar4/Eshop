using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class ProductPriceEntity : BaseTrackedModel, IBaseEntity
    {
        public long Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid ProductId { get; set; }
        public ProductEntity Product { get; set; }
    }
}