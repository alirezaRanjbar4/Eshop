using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class CustomerStoreDTO : BaseDto
    {
        public Guid CustomerId { get; set; }
        public Guid StoreId { get; set; }
    }
}