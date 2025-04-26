using AutoMapper;
using Eshop.Application.DTO.Models.Category;
using Eshop.Application.DTO.Models.Product;
using Eshop.Application.DTO.Models.Service;
using Eshop.Domain.Models;
using Eshop.Share.Exceptions;

namespace Eshop.Application.Mapping.Models
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryEntity, CategoryDTO>()
                .ForMember(des => des.String_Type, option => option.MapFrom(src => src.Type.GetEnumDescription()))
                .ReverseMap();

            CreateMap<ProductCategoryEntity, ProductCategoryDTO>().ReverseMap();

            CreateMap<ServiceCategoryEntity, ServiceCategoryDTO>().ReverseMap();
        }
    }
}
