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

            CreateMap<VendorEntity, VendorUserDTO>()
                .ForMember(des => des.UserName, option => option.MapFrom(src => src.User != null ? src.User.UserName : string.Empty))
                .ForMember(des => des.Email, option => option.MapFrom(src => src.User != null ? src.User.Email : string.Empty))
                .ForMember(des => des.PhoneNumber, option => option.MapFrom(src => src.User != null ? src.User.PhoneNumber : string.Empty))
                .ForMember(des => des.String_Store, option => option.MapFrom(src => src.Store != null ? src.Store.Name : string.Empty));


            CreateMap<VendorUserDTO, VendorDTO>();

            CreateMap<VendorUserDTO, AddUserDTO>();

            CreateMap<VendorUserDTO, EditUserDTO>();
        }
    }
}
