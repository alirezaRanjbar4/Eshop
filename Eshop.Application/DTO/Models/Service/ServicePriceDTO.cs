using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Service
{
    public class ServicePriceDTO : BaseDTO
    {
        public long Price { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public Guid? ServiceId { get; set; }
    }
}