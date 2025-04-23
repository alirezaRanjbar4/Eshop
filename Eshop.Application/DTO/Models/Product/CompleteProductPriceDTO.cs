using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class CompleteProductPriceDTO : BaseDTO
    {
        public long Price { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }

        public Guid ProductId { get; set; }
    }
}