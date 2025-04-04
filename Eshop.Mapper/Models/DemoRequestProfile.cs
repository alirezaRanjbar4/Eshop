using AutoMapper;
using Eshop.DTO.Models.DemoRequest;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
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
