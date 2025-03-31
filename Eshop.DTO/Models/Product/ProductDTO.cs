using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.Product
{
    public class ProductDTO : BaseDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public MeasurementUnit MeasurementUnit { get; set; }
        public bool OpenToSell { get; set; }
        public Guid StoreId { get; set; }
        public long Price { get; set; }

        public List<Guid> ProductCategoryIds { get; set; }
    }
}