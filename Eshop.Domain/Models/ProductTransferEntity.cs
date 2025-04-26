using Eshop.Domain.General;
using Eshop.Share.Enum;

namespace Eshop.Domain.Models
{
    public class ProductTransferEntity : BaseTrackedModel, IBaseEntity
    {
        public int Count { get; set; }
        public string? Description { get; set; }
        public ProductTransferType Type { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        public Guid WarehouseLocationId { get; set; }
        public virtual WarehouseLocationEntity WarehouseLocation { get; set; }

        //public Guid? DocumentId { get; set; }
        //public virtual DocumentEntity? Document { get; set; }
    }
}