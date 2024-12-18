using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.User;

namespace Eshop.DTO.Identities.Authentication
{
    public class LoginDTO
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool NeedCaptcha { get; set; }
        public string Token { get; set; }
        public List<LoginUserRolesDTO> UserRoles { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<ControllerDTO>? Claims { get; set; }
        public string SecureClaims { get; set; }
    }
}
