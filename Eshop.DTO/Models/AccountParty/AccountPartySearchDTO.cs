using Eshop.DTO.General;
using Eshop.Enum;

namespace Eshop.DTO.Models.AccountParty
{
    public class AccountPartySearchDTO : BaseSearchByIdDTO
    {
        public AccountPartyType Type { get; set; }
    }
}