using Eshop.DTO.General;

namespace Eshop.DTO.Models.Vendor
{
    public class VendorUserDTO : BaseDto
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PassWord { get; set; }
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public Guid RoleId { get; set; }
    }
}