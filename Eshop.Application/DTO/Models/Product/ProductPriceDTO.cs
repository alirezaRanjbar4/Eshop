using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class ProductPriceDTO : BaseDTO
    {
        public long Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid? ProductId { get; set; }
    }
}