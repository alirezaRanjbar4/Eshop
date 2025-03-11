using Eshop.DTO.General;

namespace Eshop.DTO.Models.Service
{
    public class ServicePriceDTO : BaseDto
    {
        public long Price { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Guid? ServiceId { get; set; }
    }
}