using Eshop.DTO.Identities.RoleClaim;

namespace Eshop.DTO.Identities.DynamicAccess
{
    public class DynamicAccessDTO
    {
        public Guid RoleId { set; get; }
        public string RoleName { set; get; }
        public List<ControllerDTO> SecuredControllerActions { set; get; }
        public List<GetRoleClaimDTO> RoleClaims { set; get; }
        public List<GetRoleClaimDTO> BaseRoleClaims { set; get; }
    }
}
