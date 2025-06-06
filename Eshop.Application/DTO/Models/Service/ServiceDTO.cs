﻿using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Service
{
    public class ServiceDTO : BaseDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid StoreId { get; set; }
        public long Price { get; set; }

        public List<Guid> ServiceCategoryIds { get; set; }
    }
}