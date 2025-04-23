using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Product
{
    public class GetAllProductWarehouseLocationDTO : BaseDTO
    {
        public float Count { get; set; }
        public string WarehouseLocation { get; set; }
        public string Warehouse { get; set; }
        public string Product { get; set; }
    }
}