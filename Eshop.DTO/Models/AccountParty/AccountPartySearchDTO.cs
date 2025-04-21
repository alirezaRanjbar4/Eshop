using Eshop.Common.Enum;
using Eshop.DTO.General;

namespace Eshop.DTO.Models.AccountParty
{
    public class AccountPartySearchDTO : BaseSearchDTO
    {
        public AccountPartyType? Type { get; set; }
    }
}