using AutoMapper;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategoryEntity, ProductCategoryDTO>().ReverseMap();
        }
    }
}
