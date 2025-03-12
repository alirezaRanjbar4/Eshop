using Eshop.DTO.General;

namespace Eshop.DTO.Models.Supplier
{
    public class SupplierDTO : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid StoreId { get; set; }
    }
}