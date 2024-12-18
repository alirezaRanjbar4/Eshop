using Eshop.DTO.General;

namespace Eshop.DTO.Identities.RoleClaim
{
    public class GetRoleClaimDTO : BaseDto
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string RoutePath { get; set; }
    }
}
