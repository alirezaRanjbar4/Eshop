using AutoMapper;
using Eshop.DTO.Models.Service;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ServiceCategoryProfile : Profile
    {
        public ServiceCategoryProfile()
        {
            CreateMap<ServiceCategoryEntity, ServiceCategoryDTO>().ReverseMap();
        }
    }
}
