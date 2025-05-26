using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class GetReceiptDTO : BaseDTO
    {
        public GetReceiptDTO()
        {
            ProductItems = new List<GetReceiptProductItemDTO>();
            ServiceItems = new List<GetReceiptServiceItemDTO>();
        }
        public int ReceiptNumber { get; set; }
        public string? ReceiptSerial { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public bool IsFinalized { get; set; }
        public ReceiptType Type { get; set; }

        public Guid AccountPartyId { get; set; }
        public Guid StoreId { get; set; }

        public List<GetReceiptProductItemDTO> ProductItems { get; set; }
        public List<GetReceiptServiceItemDTO> ServiceItems { get; set; }

        public string String_Date { get; set; }
        public string String_AccountParty { get; set; }

        public long TotalPrice { get; set; }
        public long TotalDiscountPrice { get; set; }
        public long TotalValueAddedPrice { get; set; }
        public long TotalFinalPrice { get; set; }

        public string String_TotalPrice { get; set; }
        public string String_TotalDiscountPrice { get; set; }
        public string String_TotalValueAddedPrice { get; set; }
        public string String_TotalFinalPrice { get; set; }
    }
}