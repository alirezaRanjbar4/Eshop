using Eshop.DTO.General;

namespace Eshop.DTO.Models.Vendor
{
    public class VendorDTO : BaseDto
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
    }
}