using Eshop.DTO.General;

namespace Eshop.DTO.Models.Customer
{
    public class CustomerDTO : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid StoreId { get; set; }
    }
}