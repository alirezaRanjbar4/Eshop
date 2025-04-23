using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Warehouse
{
    public class WarehouseInventoryDTO : BaseDTO
    {
        public string Product { get; set; }
        public string WarehouseLocation { get; set; }
        public float Count { get; set; }
    }
}