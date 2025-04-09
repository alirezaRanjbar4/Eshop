using Eshop.DTO.General;

namespace Eshop.DTO.Identities.User
{
    public class ChangeUserPasswordDTO : BaseDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
