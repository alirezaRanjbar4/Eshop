using AutoMapper;
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
                .ReverseMap();

            CreateMap<ServiceEntity, GetServiceDTO>()
               .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null ? src.ServicePrices.Last().Price : 0))
               .ReverseMap();

            CreateMap<ServiceEntity, GetAllServiceDTO>()
                .ForMember(des => des.Category, option => option.MapFrom(src => src.ServiceCategories != null ? src.ServiceCategories.Select(x => x.Category.Name).ToString() : string.Empty))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null ? src.ServicePrices.Last().Price : 0));
        }
    }
}
