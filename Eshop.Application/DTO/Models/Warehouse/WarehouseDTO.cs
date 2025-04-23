using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Warehouse
{
    public class WarehouseDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsLock { get; set; }
        public Guid StoreId { get; set; }
    }
}