using Eshop.DTO.General;

namespace Eshop.DTO.Models.Service
{
    public class GetAllServiceDTO : BaseDto
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public long Price { get; set; }
    }
}