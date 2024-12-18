using AutoMapper;
using Eshop.DTO.Identities.RoleClaim;
using Eshop.Entity.Identities;

namespace Eshop.Mapper.Identities
{
    public class RoleClaimProfile : Profile
    {
        public RoleClaimProfile()
        {
            CreateMap<RoleClaimEntity, GetRoleClaimDTO>();
        }
    }
}
