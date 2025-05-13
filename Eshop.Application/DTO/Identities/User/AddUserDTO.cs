using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Identities.User
{
    public class AddUserDTO : BaseDTO
    {
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassWord { get; set; }
    }
}
