using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Service
{
    public class GetAllServiceDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public long Price { get; set; }
    }
}