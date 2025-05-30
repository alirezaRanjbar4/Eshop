﻿using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class WarehouseLocationEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public int LocationNumber { get; set; }

        public Guid WarehouseId { get; set; }
        public virtual WarehouseEntity Warehouse { get; set; }

        public virtual ICollection<ProductWarehouseLocationEntity> ProductWarehouseLocations { get; set; }
        public virtual ICollection<ProductTransferEntity> ProductTransfers { get; set; }
        public virtual ICollection<ReceiptProductItemEntity> ReceiptProductItems { get; set; }
        public virtual ICollection<TransferReceiptItemEntity> EnteredTransferReceiptItems { get; set; }
        public virtual ICollection<TransferReceiptItemEntity> ExitedTransferReceiptItems { get; set; }
    }
}