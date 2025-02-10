using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ImageEntity, ImageDTO>().ReverseMap();
        }
    }
}
