using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class ImageDTO : BaseDTO
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public Guid ProductId { get; set; }
    }
}