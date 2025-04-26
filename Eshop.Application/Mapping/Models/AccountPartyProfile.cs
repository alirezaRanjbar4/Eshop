using AutoMapper;
using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Models.AccountParty;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
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
