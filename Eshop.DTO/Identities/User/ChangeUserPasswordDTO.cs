using Eshop.DTO.General;

namespace Eshop.DTO.Identities.User
{
    public class ChangeUserPasswordDTO : BaseDto
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
