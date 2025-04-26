using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.AccountParty
{
    public class AccountPartySearchDTO : BaseSearchDTO
    {
        public AccountPartyType? Type { get; set; }
    }
}