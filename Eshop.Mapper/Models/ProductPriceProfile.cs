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
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => src.ExpiryDate.HasValue ? Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate.Value, false) : string.Empty))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));
        }
    }
}
