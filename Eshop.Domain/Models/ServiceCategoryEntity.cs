﻿using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class ServiceCategoryEntity : BaseTrackedModel, IBaseEntity
    {
        public Guid ServiceId { get; set; }
        public virtual ServiceEntity Service { get; set; }

        public Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
    }
}