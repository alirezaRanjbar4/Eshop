using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class ImageDTO : BaseDto
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public Guid ProductId { get; set; }
    }
}