using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.Product
{
    public class GetProductDTO : BaseDto
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