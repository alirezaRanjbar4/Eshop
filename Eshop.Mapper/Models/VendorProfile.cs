using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<VendorEntity, VendorDTO>().ReverseMap();
        }
    }
}
