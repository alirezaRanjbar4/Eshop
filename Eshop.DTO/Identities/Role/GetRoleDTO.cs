using Eshop.DTO.General;
using Eshop.DTO.Identities.RoleClaim;

namespace Eshop.DTO.Identities.Role
{
    public class GetRoleDTO : BaseDto
    {
        public string Name { get; set; }
        public List<GetRoleClaimDTO> RoleClaims { get; set; }
        public List<GetRoleClaimDTO> BaseRoleClaim { get; set; }
    }
}
