using Eshop.DTO.General;

namespace Eshop.DTO.Models.Warehouse
{
    public class WarehouseDTO : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLock { get; set; }
        public Guid StoreId { get; set; }
    }
}