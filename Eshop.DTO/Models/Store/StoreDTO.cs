using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.Store
{
    public class StoreDTO : BaseDTO
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        public StoreType StoreType { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}