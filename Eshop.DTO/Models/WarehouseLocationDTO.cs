using Eshop.DTO.General;

namespace Eshop.DTO.Models
{
    public class WarehouseLocationDTO : BaseDto
    {
        public string Name { get; set; }
        public int LocationNumber { get; set; }
        public Guid WarehouseId { get; set; }

        public List<ProductWarehouseLocationDTO> ProductWarehouseLocations { get; set; }
    }
}