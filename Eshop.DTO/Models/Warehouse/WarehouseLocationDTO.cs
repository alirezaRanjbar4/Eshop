using Eshop.DTO.General;

namespace Eshop.DTO.Models.Warehouse
{
    public class WarehouseLocationDTO : BaseDTO
    {
        public string Name { get; set; }
        public int LocationNumber { get; set; }
        public Guid WarehouseId { get; set; }
    }
}