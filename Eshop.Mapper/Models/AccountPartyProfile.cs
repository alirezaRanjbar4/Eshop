using AutoMapper;
using Eshop.DTO.General;
using Eshop.DTO.Models.AccountParty;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class AccountPartyProfile : Profile
    {
        public AccountPartyProfile()
        {
            CreateMap<AccountPartyEntity, AccountPartyDTO>().ReverseMap();

            CreateMap<AccountPartyEntity, SimpleDTO>()
                .ForMember(des => des.Key, option => option.MapFrom(src => src.Name))
                .ForMember(des => des.Value, option => option.MapFrom(src => src.Id));
        }
    }
}
