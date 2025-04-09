using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class GetAllProductWarehouseLocationDTO : BaseDTO
    {
        public float Count { get; set; }
        public string WarehouseLocation { get; set; }
        public string Warehouse { get; set; }
        public string Product { get; set; }
    }
}