using Eshop.DTO.General;

namespace Eshop.DTO.Models.Vendor
{
    public class GetAllVendorDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string StoreName { get; set; }
    }
}