using Eshop.DTO.General;

namespace Eshop.DTO.Models.Warehouse
{
    public class WarehouseLocationDTO : BaseDto
    {
        public string Name { get; set; }
        public int LocationNumber { get; set; }
        public Guid WarehouseId { get; set; }
    }
}