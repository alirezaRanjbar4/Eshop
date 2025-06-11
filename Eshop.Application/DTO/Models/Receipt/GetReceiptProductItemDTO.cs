using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class GetReceiptProductItemDTO : BaseDTO
    {
        public string? Description { get; set; }
        public long Count { get; set; }
        public long Price { get; set; }
        public long? DiscountPrice { get; set; }
        public int? DiscountPercent { get; set; }
        public int? ValueAddedPercent { get; set; }

        public Guid ReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public Guid? WarehouseLocationId { get; set; }

        public string String_Warehouse { get;set; }
        public string String_Product { get;set; }

        public long TotalPrice { get;set; }
        public long FinalDiscountPrice { get;set; }
        public long TotalPriceAfterDiscount { get;set; }
        public long ValueAddedPrice { get;set; }
        public long TotalFinalPrice { get;set; }

        public string String_TotalPrice { get; set; }
        public string String_FinalDiscountPrice { get; set; }
        public string String_TotalPriceAfterDiscount { get; set; }
        public string String_ValueAddedPrice { get; set; }
        public string String_TotalFinalPrice { get; set; }
    }
}