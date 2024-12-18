using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class CustomerDTO : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public List<CustomerStoreDTO> CustomerStores { get; set; }
        public List<ShoppingCardItemDTO> ShoppingCardItems { get; set; }
        public List<OrderDTO> Orders { get; set; }
    }
}