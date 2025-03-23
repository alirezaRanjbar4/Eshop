using AutoMapper;
using Eshop.DTO.Models.AccountParty;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class AccountPartyProfile : Profile
    {
        public AccountPartyProfile()
        {
            CreateMap<AccountPartyEntity, AccountPartyDTO>().ReverseMap();
        }
    }
}
