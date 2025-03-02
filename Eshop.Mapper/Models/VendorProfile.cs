using AutoMapper;
using Eshop.DTO.Identities.User;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class VendorProfile : Profile
    {
        public VendorProfile()
        {
            CreateMap<VendorEntity, VendorDTO>().ReverseMap();

            CreateMap<VendorUserDTO, AddUserDTO>();

            CreateMap<VendorUserDTO, EditUserDTO>();
        }
    }
}
