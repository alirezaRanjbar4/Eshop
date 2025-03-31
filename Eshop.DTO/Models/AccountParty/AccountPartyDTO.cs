using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.AccountParty
{
    public class AccountPartyDTO : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public AccountPartyType Type { get; set; }
        public Guid StoreId { get; set; }
    }
}