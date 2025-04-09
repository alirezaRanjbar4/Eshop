using Eshop.DTO.General;

namespace Eshop.DTO.Identities.User
{
    public class EditUserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassWord { get; set; }
    }
}
