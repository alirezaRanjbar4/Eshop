using AutoMapper;
using Eshop.DTO.Models.Product;
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
