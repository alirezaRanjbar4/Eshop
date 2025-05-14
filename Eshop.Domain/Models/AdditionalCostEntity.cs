using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class AdditionalCostEntity : BaseTrackedModel, IBaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public long Amount { get; set; }
        public DateTime Date { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }
    }
}