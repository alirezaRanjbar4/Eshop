using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Identities.User
{
    public class ChangeUserPasswordDTO : BaseDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
