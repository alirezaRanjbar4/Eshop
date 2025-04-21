using Eshop.Common.Enum;
using Eshop.Entity.General;

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
        public virtual ICollection<ImageEntity> Images { get; set; }
        public virtual ICollection<ProductPriceEntity> ProductPrices { get; set; }
        public virtual ICollection<ProductTransferEntity> ProductTransfers { get; set; }
        public virtual ICollection<ReceiptProductItemEntity> ReceiptProductItems { get; set; }
        public virtual ICollection<ReceiptServiceItemEntity> ReceiptServiceItems { get; set; }
        public virtual ICollection<TransferReceiptItemEntity> TransferReceiptItems { get; set; }

    }
}