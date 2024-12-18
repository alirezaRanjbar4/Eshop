using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class WarehouseLocationProfile : Profile
    {
        public WarehouseLocationProfile()
        {
            CreateMap<WarehouseLocationEntity, WarehouseLocationDTO>().ReverseMap();
        }
    }
}
