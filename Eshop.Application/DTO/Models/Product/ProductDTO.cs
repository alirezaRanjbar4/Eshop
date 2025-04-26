using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Product
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