using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Identities.User
{
    public class EditUserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassWord { get; set; }
    }
}
