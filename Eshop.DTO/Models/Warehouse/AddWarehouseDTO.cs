using Eshop.DTO.General;

namespace Eshop.DTO.Models.Warehouse
{
    public class AddWarehouseDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLock { get; set; }
        public Guid StoreId { get; set; }

        public List<WarehouseLocationDTO> WarehouseLocations { get; set; }
    }
}