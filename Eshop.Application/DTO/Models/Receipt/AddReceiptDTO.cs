﻿using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.Receipt
{
    public class AddReceiptDTO : BaseDTO
    {
        public DateTime Date { get; set; }
        public int ReceiptNumber { get; set; }
        public string? ReceiptSerial { get; set; }
        public string? Description { get; set; }
        public ReceiptType Type { get; set; }
        public Guid AccountPartyId { get; set; }
        public Guid StoreId { get; set; }
        public List<ReceiptProductItemDTO> ProductItems { get; set; }
        public List<ReceiptServiceItemDTO> ServiceItems { get; set; }
    }
}