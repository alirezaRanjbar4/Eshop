using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<WarehouseEntity, WarehouseDTO>().ReverseMap();
        }
    }
}
