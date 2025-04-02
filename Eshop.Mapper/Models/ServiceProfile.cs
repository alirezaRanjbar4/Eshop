using AutoMapper;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Models.Service;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ServiceEntity, ServiceDTO>()
                .ForMember(des => des.ServiceCategoryIds, option => option.MapFrom(src => src.ServiceCategories != null ? src.ServiceCategories.Select(x => x.CategoryId) : null))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null ? src.ServicePrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
                .ReverseMap();

            CreateMap<ServiceEntity, GetServiceDTO>()
               .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null ? src.ServicePrices.Last().Price : 0))
               .ReverseMap();

            CreateMap<ServiceEntity, GetAllServiceDTO>()
                .ForMember(des => des.Category, option => option.MapFrom(src => src.ServiceCategories != null ? string.Join(",", src.ServiceCategories.Select(x => x.Category.Name)) : string.Empty))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null ? src.ServicePrices.Last().Price : 0));

            CreateMap<ServicePriceEntity, ServicePriceDTO>().ReverseMap();

            CreateMap<ServicePriceEntity, CompleteServicePriceDTO>()
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => src.ExpiryDate.HasValue ? Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate.Value, false) : string.Empty))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));
        }
    }
}
