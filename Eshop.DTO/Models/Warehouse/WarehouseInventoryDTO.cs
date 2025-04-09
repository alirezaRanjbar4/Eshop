using Eshop.DTO.General;

namespace Eshop.DTO.Models.Warehouse
{
    public class WarehouseInventoryDTO : BaseDTO
    {
        public string Product { get; set; }
        public string WarehouseLocation { get; set; }
        public float Count { get; set; }
    }
}