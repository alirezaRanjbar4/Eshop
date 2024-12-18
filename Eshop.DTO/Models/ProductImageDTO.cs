using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class ProductImageDTO : BaseDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Guid ProductId { get; set; }
    }
}