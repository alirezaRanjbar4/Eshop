using AutoMapper;
using Eshop.DTO.Models.Supplier;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<SupplierEntity, SupplierDTO>().ReverseMap();
        }
    }
}
