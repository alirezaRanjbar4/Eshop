﻿using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class ServicePriceEntity : BaseTrackedModel, IBaseEntity
    {
        public long Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid ServiceId { get; set; }
        public ServiceEntity Service { get; set; }
    }
}