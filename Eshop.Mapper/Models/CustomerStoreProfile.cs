using AutoMapper;
using Eshop.DTO.Models;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class CustomerStoreProfile : Profile
    {
        public CustomerStoreProfile()
        {
            CreateMap<CustomerStoreEntity, CustomerStoreDTO>().ReverseMap();
        }
    }
}
