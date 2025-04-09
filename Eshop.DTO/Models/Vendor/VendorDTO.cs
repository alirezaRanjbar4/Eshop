using Eshop.DTO.General;

namespace Eshop.DTO.Models.Vendor
{
    public class VendorDTO : BaseDTO
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
    }
}