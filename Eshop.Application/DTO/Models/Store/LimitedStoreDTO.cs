using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Store
{
    public class LimitedStoreDTO : BaseDTO
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}