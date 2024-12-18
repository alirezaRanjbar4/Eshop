using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ProductWarehouseLocationProfile : Profile
    {
        public ProductWarehouseLocationProfile()
        {
            CreateMap<ProductWarehouseLocationEntity, ProductWarehouseLocationDTO>().ReverseMap();
        }
    }
}
