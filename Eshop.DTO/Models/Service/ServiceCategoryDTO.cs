using Eshop.DTO.General;

namespace Eshop.DTO.Models.Service
{
    public class ServiceCategoryDTO : BaseDTO
    {
        public Guid ServiceId { get; set; }
        public Guid CategoryId { get; set; }
    }
}