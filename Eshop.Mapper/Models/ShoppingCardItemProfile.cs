using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ShoppingCardItemProfile : Profile
    {
        public ShoppingCardItemProfile()
        {
            CreateMap<ShoppingCardItemEntity, ShoppingCardItemDTO>().ReverseMap();
        }
    }
}
