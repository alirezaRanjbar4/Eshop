using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models
{
    public class ProductDTO : BaseDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public bool OpenToSell { get; set; }
        public Guid StoreId { get; set; }

        public List<ProductCategoryDTO> ProductCategories { get; set; }
        public List<ProductWarehouseLocationDTO> ProductWarehouseLocations { get; set; }
    }
}