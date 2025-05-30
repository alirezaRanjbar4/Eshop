﻿using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class ProductWarehouseLocationEntity : BaseTrackedModel, IBaseEntity
    {
        public float Count { get; set; }

        public Guid WarehouseLocationId { get; set; }
        public virtual WarehouseLocationEntity WarehouseLocation { get; set; }

        public Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
    }
}