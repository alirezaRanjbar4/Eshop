using AutoMapper;
using Eshop.DTO.Models.Customer;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, CustomerDTO>().ReverseMap();
        }
    }
}
