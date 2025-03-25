using Eshop.Entity.General;
using Eshop.Enum;

namespace Eshop.Entity.Models
{
    public class StoreEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public StoreType StoreType { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
        public virtual ICollection<ServiceEntity> Services { get; set; }
        public virtual ICollection<WarehouseEntity> Warehouses { get; set; }
        public virtual ICollection<VendorEntity> Vendors { get; set; }
        public virtual ICollection<AccountPartyEntity> AccountParties { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<CategoryEntity> Categories { get; set; }
        public virtual ICollection<ReceiptEntity> Receipts { get; set; }
        public virtual ICollection<TransferReceiptEntity> TransferReceipts { get; set; }
        public virtual ICollection<FinancialDocumentEntity> FinancialDocuments { get; set; }
    }
}