using Eshop.DTO.General;

namespace Eshop.DTO.Models.Service
{
    public class CompleteServicePriceDTO : BaseDto
    {
        public long Price { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }

        public Guid ServiceId { get; set; }
    }
}