using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Store
{
    public class LimitedStoreDTO : BaseDto
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}