using AutoMapper;
using Eshop.Common.Exceptions;
using Eshop.DTO.Models.Category;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDTO>()
                .ForMember(des => des.String_Type, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                .ReverseMap();
        }
    }
}
