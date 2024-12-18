using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
        }
    }
}
