using AutoMapper;
using Eshop.Application.DTO.Identities.RoleClaim;
using Eshop.Domain.Identities;

namespace Eshop.Application.Mapping.Identities
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimEntity, GetRoleClaimDTO>();
        }
    }
}
