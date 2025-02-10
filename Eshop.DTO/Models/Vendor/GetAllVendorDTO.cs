using Eshop.DTO.General;

namespace Eshop.DTO.Models.Vendor
{
    public class GetAllVendorDTO : BaseDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
    }
}