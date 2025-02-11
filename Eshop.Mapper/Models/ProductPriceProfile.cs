using AutoMapper;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ProductPriceProfile : Profile
    {
        public ProductPriceProfile()
        {
            CreateMap<ProductPriceEntity, ProductPriceDTO>().ReverseMap();

            CreateMap<ProductPriceEntity, CompleteProductPriceDTO>()
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate, false)))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));
        }
    }
}
