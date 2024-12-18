using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreEntity, StoreDTO>().ReverseMap();
        }
    }
}
