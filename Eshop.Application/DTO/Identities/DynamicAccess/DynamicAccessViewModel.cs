using Eshop.Application.DTO.Identities.Role;

namespace Eshop.Application.DTO.Identities.DynamicAccess
{
    public class DynamicAccessIndexViewModel
    {
        public string ActionIds { set; get; }
        public Guid RoleId { set; get; }
        public GetRoleDTO RoleIncludeRoleClaims { set; get; }
        public ICollection<ControllerViewModel> SecuredControllerActions { set; get; }
    }
}
