using Eshop.DTO.General;

namespace Eshop.DTO.Models.Store
{
    public class StoreDTO : BaseDto
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}