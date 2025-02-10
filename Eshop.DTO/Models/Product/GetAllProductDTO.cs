using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Product
{
    public class GetAllProductDTO : BaseDto
    {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string MeasurementUnit { get; set; }
        public bool OpenToSell { get; set; }
        public string Category { get; set; }
        public int TotalCount { get; set; }
        public long Price { get; set; }

    }
}