using Eshop.DTO.General;
using Eshop.DTO.Models.Vendor;

namespace Eshop.DTO.Models.Store
{
    public class GetStoreDetailDTO : BaseDto
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public List<VendorDTO> Vendors { get; set; }
    }
}