using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Vendor
{
    public class VendorUserDTO : BaseDTO
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassWord { get; set; }
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public Guid RoleId { get; set; }

        public string? String_Store { get; set; }
    }
}