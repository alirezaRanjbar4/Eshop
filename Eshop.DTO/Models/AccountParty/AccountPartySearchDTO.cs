using Eshop.DTO.General;
using Eshop.Common.Enum;

namespace Eshop.DTO.Models.AccountParty
{
    public class AccountPartySearchDTO : BaseSearchDTO
    {
        public AccountPartyType? Type { get; set; }
    }
}