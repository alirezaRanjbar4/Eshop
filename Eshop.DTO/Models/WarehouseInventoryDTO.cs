using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class WarehouseInventoryDTO : BaseDto
    {
        public string Product { get; set; }
        public string WarehouseLocation { get; set; }
        public int Count { get; set; }
    }
}