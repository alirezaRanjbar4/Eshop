using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class StoreDTO : BaseDto
    {
        public string Name { get; set; }
        public string EnglishName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }

        public List<ProductDTO> Products { get; set; }
        public List<WarehouseDTO> Warehouses { get; set; }
        public List<VendorDTO> Vendors { get; set; }
        public List<CustomerStoreDTO> CustomerStores { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}