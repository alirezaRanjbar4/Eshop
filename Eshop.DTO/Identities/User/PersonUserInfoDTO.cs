using Eshop.DTO.General;

namespace Eshop.DTO.Identities.User
{
    public class PersonUserInfoDTO : BaseDto
    {
        public string PersonFullName { get; set; }
        public string UserName { get; set; }
        public string Passsword { get; set; }
        public bool IsActive { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string? ProductionPasswordHash { get; set; }

    }
}
