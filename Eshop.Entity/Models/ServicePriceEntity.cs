using Eshop.Entity.General;

namespace Eshop.Entity.Models
{
    public class ServicePriceEntity : BaseTrackedModel, IBaseEntity
    {
        public long Price { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Guid? ServiceId { get; set; }
        public virtual ServiceEntity? Service { get; set; }
    }
}