using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Service
{
    public class ServiceCategoryDTO : BaseDTO
    {
        public Guid ServiceId { get; set; }
        public Guid CategoryId { get; set; }
    }
}