using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class ProductDTO : BaseDTO
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