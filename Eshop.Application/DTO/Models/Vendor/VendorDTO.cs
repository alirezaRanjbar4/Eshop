using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Vendor
{
    public class VendorDTO : BaseDTO
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
    }
}