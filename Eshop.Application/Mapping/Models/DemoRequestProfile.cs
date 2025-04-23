using AutoMapper;
using Eshop.Application.DTO.Models.DemoRequest;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
{
    public class DemoRequestProfile : Profile
    {
        public DemoRequestProfile()
        {
            CreateMap<DemoRequestEntity, DemoRequestDTO>().ReverseMap();

            CreateMap<LimitedDemoRequestDTO, DemoRequestEntity>();
        }
    }
}
