﻿using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Product
{
    public class GetProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public bool OpenToSell { get; set; }
        public long Price { get; set; }

        public virtual ICollection<ProductCategoryDTO> ProductCategories { get; set; }
        public virtual ICollection<GetAllProductWarehouseLocationDTO> ProductWarehouseLocations { get; set; }
        public virtual ICollection<ImageDTO> Images { get; set; }
        public virtual ICollection<CompleteProductPriceDTO> ProductPrices { get; set; }
        public virtual ICollection<CompleteProductTransferDTO> ProductTransfers { get; set; }
    }
}