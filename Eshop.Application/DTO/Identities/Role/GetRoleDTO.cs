using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Identities.RoleClaim;

namespace Eshop.Application.DTO.Identities.Role
{
    public class GetRoleDTO : BaseDTO
    {
        public string Name { get; set; }
        public List<GetRoleClaimDTO> RoleClaims { get; set; }
        public List<GetRoleClaimDTO> BaseRoleClaim { get; set; }
    }
}
