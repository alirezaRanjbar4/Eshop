using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class ProductPriceDTO : BaseDto
    {
        public long Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid? ProductId { get; set; }
    }
}