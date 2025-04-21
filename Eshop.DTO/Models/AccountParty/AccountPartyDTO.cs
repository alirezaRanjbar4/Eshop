using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.AccountParty
{
    public class AccountPartyDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public AccountPartyType Type { get; set; }
        public Guid StoreId { get; set; }
    }
}