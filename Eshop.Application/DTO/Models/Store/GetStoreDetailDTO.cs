using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Models.Vendor;

namespace Eshop.Application.DTO.Models.Store
{
    public class GetStoreDetailDTO : BaseDTO
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public List<VendorDTO> Vendors { get; set; }
    }
}