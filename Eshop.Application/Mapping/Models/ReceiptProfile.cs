using AutoMapper;
using Eshop.Application.DTO.Models.Receipt;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Utilities;

namespace Eshop.Application.Mapping.Models
{
    public class ReceiptProfile : Profile
    {
        public ReceiptProfile()
        {
            CreateMap<ReceiptEntity, ReceiptDTO>().ReverseMap();

            CreateMap<ReceiptEntity, GetReceiptDTO>()
                .ForMember(des => des.String_AccountParty, option => option.MapFrom(src => src.AccountParty != null ? src.AccountParty.Name : string.Empty))
                .ForMember(des => des.String_Date, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)))
                .AfterMap((src, des) =>
                {
                    des.TotalPrice = des.ProductItems.Sum(x => x.TotalPrice) + des.ServiceItems.Sum(x => x.TotalPrice);
                    des.String_TotalPrice = des.TotalPrice.ToString("N0");

                    des.TotalDiscountPrice = des.ProductItems.Sum(x => x.FinalDiscountPrice) + des.ServiceItems.Sum(x => x.FinalDiscountPrice);
                    des.String_TotalDiscountPrice = des.TotalDiscountPrice.ToString("N0");

                    des.TotalValueAddedPrice = des.ProductItems.Sum(x => x.ValueAddedPrice) + des.ServiceItems.Sum(x => x.ValueAddedPrice);
                    des.String_TotalValueAddedPrice = des.TotalValueAddedPrice.ToString("N0");

                    des.TotalFinalPrice = des.ProductItems.Sum(x => x.TotalFinalPrice) + des.ServiceItems.Sum(x => x.TotalFinalPrice);
                    des.String_TotalFinalPrice = des.TotalFinalPrice.ToString("N0");
                });


            CreateMap<ReceiptEntity, SimpleReceiptDTO>()
                .ForMember(des => des.AccountParty, option => option.MapFrom(src => src.AccountParty != null ? src.AccountParty.Name : string.Empty));

            CreateMap<ReceiptEntity, GetAllReceiptDTO>()
                .ForMember(des => des.String_AccountParty, option => option.MapFrom(src => src.AccountParty != null ? src.AccountParty.Name : string.Empty))
                .ForMember(des => des.String_Date, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)))
                .AfterMap((src, des) =>
                {
                    long totalFinalPrice = 0;
                    if (src.ProductItems != null && src.ProductItems.Any())
                    {
                        foreach (var item in src.ProductItems)
                        {
                            var Price = item.Price * item.Count;
                            var DiscountPrice = item.DiscountPrice.HasValue ? item.DiscountPrice.Value : 0;
                            if (item.DiscountPercent.HasValue)
                            {
                                DiscountPrice = Price * item.DiscountPercent.Value / 100;
                            }

                            var ValueAddedPrice = item.ValueAddedPercent.HasValue ? (Price - DiscountPrice) * item.ValueAddedPercent.Value / 100 : 0;
                            var final = Price - DiscountPrice + ValueAddedPrice;
                            totalFinalPrice += final;
                        }
                    }

                    if (src.ServiceItems != null && src.ServiceItems.Any())
                    {
                        foreach (var item in src.ServiceItems)
                        {
                            var Price = item.Price * item.Count;
                            var DiscountPrice = item.DiscountPrice.HasValue ? item.DiscountPrice.Value : 0;
                            if (item.DiscountPercent.HasValue)
                            {
                                DiscountPrice = Price * item.DiscountPercent.Value / 100;
                            }

                            var ValueAddedPrice = item.ValueAddedPercent.HasValue ? (Price - DiscountPrice) * item.ValueAddedPercent.Value / 100 : 0;
                            var final = Price - DiscountPrice + ValueAddedPrice;
                            totalFinalPrice += final;
                        }
                    }

                    des.String_TotalFinalPrice = totalFinalPrice.ToString("N0");
                });

            CreateMap<AddReceiptDTO, ReceiptEntity>();

            CreateMap<AddReceiptDTO, ReceiptDTO>();

            CreateMap<ReceiptProductItemEntity, ReceiptProductItemDTO>().ReverseMap();

            CreateMap<ReceiptProductItemEntity, GetReceiptProductItemDTO>()
                .ForMember(des => des.String_Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .AfterMap((src, des) =>
                {
                    des.TotalFinalPrice = src.Price * src.Count;
                    des.String_TotalFinalPrice = des.TotalFinalPrice.ToString("N0");

                    if (src.DiscountPrice.HasValue)
                    {
                        des.FinalDiscountPrice = src.DiscountPrice.Value;
                    }
                    else if (src.DiscountPercent.HasValue)
                    {
                        des.FinalDiscountPrice = des.TotalFinalPrice * src.DiscountPercent.Value / 100;
                    }
                    else
                    {
                        des.FinalDiscountPrice = 0;
                    }
                    des.String_FinalDiscountPrice = des.FinalDiscountPrice.ToString("N0");

                    des.TotalPriceAfterDiscount = des.TotalFinalPrice - des.FinalDiscountPrice;
                    des.String_TotalPriceAfterDiscount = des.TotalPriceAfterDiscount.ToString("N0");

                    if (src.ValueAddedPercent.HasValue)
                    {
                        des.ValueAddedPrice = des.TotalPriceAfterDiscount * src.ValueAddedPercent.Value / 100;
                    }
                    else
                    {
                        des.ValueAddedPrice = 0;
                    }
                    des.String_ValueAddedPrice = des.ValueAddedPrice.ToString("N0");

                    des.TotalFinalPrice = des.TotalPriceAfterDiscount + des.ValueAddedPrice;
                    des.String_TotalFinalPrice = des.TotalFinalPrice.ToString("N0");
                });

            CreateMap<ReceiptServiceItemEntity, ReceiptServiceItemDTO>().ReverseMap();

            CreateMap<ReceiptServiceItemEntity, GetReceiptServiceItemDTO>()
                .ForMember(des => des.String_Service, option => option.MapFrom(src => src.Service != null ? src.Service.Name : string.Empty))
                .AfterMap((src, des) =>
                {
                    des.TotalFinalPrice = src.Price * src.Count;
                    des.String_TotalFinalPrice = des.TotalFinalPrice.ToString("N0");

                    if (src.DiscountPrice.HasValue)
                    {
                        des.FinalDiscountPrice = src.DiscountPrice.Value;
                    }
                    else if (src.DiscountPercent.HasValue)
                    {
                        des.FinalDiscountPrice = des.TotalFinalPrice * src.DiscountPercent.Value / 100;
                    }
                    else
                    {
                        des.FinalDiscountPrice = 0;
                    }
                    des.String_FinalDiscountPrice = des.FinalDiscountPrice.ToString("N0");

                    des.TotalPriceAfterDiscount = des.TotalFinalPrice - des.FinalDiscountPrice;
                    des.String_TotalPriceAfterDiscount = des.TotalPriceAfterDiscount.ToString("N0");

                    if (src.ValueAddedPercent.HasValue)
                    {
                        des.ValueAddedPrice = des.TotalPriceAfterDiscount * src.ValueAddedPercent.Value / 100;
                    }
                    else
                    {
                        des.ValueAddedPrice = 0;
                    }
                    des.String_ValueAddedPrice = des.ValueAddedPrice.ToString("N0");

                    des.TotalFinalPrice = des.TotalPriceAfterDiscount + des.ValueAddedPrice;
                    des.String_TotalFinalPrice = des.TotalFinalPrice.ToString("N0");
                });
        }
    }
}
