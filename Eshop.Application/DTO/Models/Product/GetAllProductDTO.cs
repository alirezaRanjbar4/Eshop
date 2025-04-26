using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class GetAllProductDTO : BaseDTO
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