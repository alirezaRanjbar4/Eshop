using Eshop.DTO.General;

namespace Eshop.DTO.Models.Product
{
    public class CompleteProductTransferDTO : BaseDTO
    {
        public int Count { get; set; }
        public string? Description { get; set; }
        public string Type { get; set; }
        public string Product { get; set; }
        public string WarehouseLocation { get; set; }
        public string Warehouse { get; set; }
        public string Date { get; set; }
    }
}