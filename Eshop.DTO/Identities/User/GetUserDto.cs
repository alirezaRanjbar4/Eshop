using Eshop.DTO.General;

namespace Eshop.DTO.Identities.User
{
    public class GetUserDto : BaseDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? IPAddress { get; set; }
        public string AccessToken { get; set; }
    }
}
