﻿using Eshop.Application.DTO.General;
using Eshop.Share.Enum;

namespace Eshop.Application.DTO.Models.AccountParty
{
    public class AccountPartyDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Description { get; set; }
        public AccountPartyType Type { get; set; }
        public Guid StoreId { get; set; }
    }
}