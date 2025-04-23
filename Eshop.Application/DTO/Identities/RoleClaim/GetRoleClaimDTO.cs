using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Identities.RoleClaim
{
    public class GetRoleClaimDTO : BaseDTO
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public string RoutePath { get; set; }
    }
}
