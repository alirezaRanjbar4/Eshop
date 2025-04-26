using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Application.DTO.Identities.User;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Identities.Authentication
{
    public class LoginDTO : BaseDTO
    {
        public string Name { get; set; }
        public UserType UserType { get; set; }

        public string? StoreName { get; set; }
        public StoreType? StoreType { get; set; }
        public Guid? StoreId { get; set; }

        public bool NeedCaptcha { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public List<LoginUserRolesDTO> UserRoles { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<ControllerDTO>? Claims { get; set; }
        public string SecureClaims { get; set; }
    }
}
