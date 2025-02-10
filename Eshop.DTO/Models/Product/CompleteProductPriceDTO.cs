using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class CompleteProductPriceDTO : BaseDto
    {
        public long Price { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }

        public Guid? ProductId { get; set; }
    }
}