using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Identities.User;

namespace Eshop.DTO.Identities.Authentication
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
        public List<LoginUserRolesDTO> UserRoles { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public List<ControllerDTO>? Claims { get; set; }
        public string SecureClaims { get; set; }
    }
}
