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

            CreateMap<AddReceiptDTO, ReceiptEntity>();

            CreateMap<AddReceiptDTO, ReceiptDTO>();

            CreateMap<ReceiptProductItemEntity, ReceiptProductItemDTO>().ReverseMap();

            CreateMap<ReceiptProductItemEntity, GetReceiptProductItemDTO>()
                .ForMember(des => des.String_Product, option => option.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                .AfterMap((src, des) =>
                {
                    des.TotalPrice = src.Price * src.Count;
                    des.String_TotalPrice = des.TotalPrice.ToString("N0");

                    if (src.DiscountPrice.HasValue)
                    {
                        des.FinalDiscountPrice = src.DiscountPrice.Value;
                    }
                    else if (src.DiscountPercent.HasValue)
                    {
                        des.FinalDiscountPrice = des.TotalPrice * src.DiscountPercent.Value / 100;
                    }
                    else
                    {
                        des.FinalDiscountPrice = 0;
                    }
                    des.String_FinalDiscountPrice = des.FinalDiscountPrice.ToString("N0");

                    des.TotalPriceAfterDiscount = des.TotalPrice - des.FinalDiscountPrice;
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
                    des.TotalPrice = src.Price * src.Count;
                    des.String_TotalPrice = des.TotalPrice.ToString("N0");

                    if (src.DiscountPrice.HasValue)
                    {
                        des.FinalDiscountPrice = src.DiscountPrice.Value;
                    }
                    else if (src.DiscountPercent.HasValue)
                    {
                        des.FinalDiscountPrice = des.TotalPrice * src.DiscountPercent.Value / 100;
                    }
                    else
                    {
                        des.FinalDiscountPrice = 0;
                    }
                    des.String_FinalDiscountPrice = des.FinalDiscountPrice.ToString("N0");

                    des.TotalPriceAfterDiscount = des.TotalPrice - des.FinalDiscountPrice;
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

            CreateMap<ReceiptEntity, GetAllReceiptDTO>()
             .ForMember(des => des.String_AccountParty, option => option.MapFrom(src => src.AccountParty != null ? src.AccountParty.Name : string.Empty))
             .ForMember(des => des.String_Date, opt => opt.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)))
             .AfterMap((src, des) =>
             {
                 var totalFinalPrice =
                     (src.ProductItems ?? []).Sum(item => CalculateFinalPrice(item.Count, item.Price, item.DiscountPercent, item.DiscountPrice, item.ValueAddedPercent))
                     +
                     (src.ServiceItems ?? []).Sum(item => CalculateFinalPrice(item.Count, item.Price, item.DiscountPercent, item.DiscountPrice, item.ValueAddedPercent));

                 des.String_TotalFinalPrice = totalFinalPrice.ToString("N0");
             });

            CreateMap<ReceiptEntity, ReportReceiptItemDTO>()
                .AfterMap((src, des) =>
                {
                    des.TotalFinalPrice =
                         (src.ProductItems ?? []).Sum(item => CalculateFinalPrice(item.Count, item.Price, item.DiscountPercent, item.DiscountPrice, item.ValueAddedPercent))
                         +
                         (src.ServiceItems ?? []).Sum(item => CalculateFinalPrice(item.Count, item.Price, item.DiscountPercent, item.DiscountPrice, item.ValueAddedPercent));
                });

            CreateMap<ReceiptServiceItemEntity, ReportReceiptItemDTO>()
                .ForMember(des => des.Type, option => option.MapFrom(src => src.Receipt.Type))
                .ForMember(des => des.Date, option => option.MapFrom(src => src.Receipt.Date))
                .AfterMap((src, des) =>
                {
                    des.TotalFinalPrice = CalculateFinalPrice(src.Count, src.Price, src.DiscountPercent, src.DiscountPrice, src.ValueAddedPercent);
                });

            CreateMap<ReceiptProductItemEntity, ReportReceiptItemDTO>()
                .ForMember(des => des.Type, option => option.MapFrom(src => src.Receipt.Type))
                .ForMember(des => des.Date, option => option.MapFrom(src => src.Receipt.Date))
                .AfterMap((src, des) =>
                {
                    des.TotalFinalPrice = CalculateFinalPrice(src.Count, src.Price, src.DiscountPercent, src.DiscountPrice, src.ValueAddedPercent);
                });
        }

        private static long CalculateFinalPrice(long count, long price, int? discountPercent, long? discountPrice, int? valueAddedPercent)
        {
            long totalPrice = count * price;

            long appliedDiscount = discountPrice ?? (discountPercent.HasValue ? (totalPrice * discountPercent.Value / 100) : 0);
            long priceAfterDiscount = totalPrice - appliedDiscount;

            long valueAdded = valueAddedPercent.HasValue ? (priceAfterDiscount * valueAddedPercent.Value / 100) : 0;

            return priceAfterDiscount + valueAdded;
        }
    }
}
