using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Service
{
    public class CompleteServicePriceDTO : BaseDTO
    {
        public long Price { get; set; }
        public string StartDate { get; set; }
        public string ExpiryDate { get; set; }

        public Guid ServiceId { get; set; }
    }
}