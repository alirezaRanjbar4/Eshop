using Eshop.DTO.General;
using Eshop.DTO.Models.Product;

namespace Eshop.DTO.Models.Service
{
    public class GetServiceDTO : BaseDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public long Price { get; set; }

        public virtual ICollection<ServiceCategoryDTO> ServiceCategories { get; set; }
        public virtual ICollection<ImageDTO> Images { get; set; }
        public virtual ICollection<CompleteServicePriceDTO> ServicePrices { get; set; }
    }
}