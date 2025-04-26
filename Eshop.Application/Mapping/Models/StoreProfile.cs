using AutoMapper;
using Eshop.Application.DTO.Models.Store;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
{
    public class StoreProfile : Profile
    {
        public StoreProfile()
        {
            CreateMap<StoreEntity, StoreDTO>().ReverseMap();

            CreateMap<LimitedStoreDTO, StoreEntity>();

            CreateMap<StorePaymentEntity, StorePaymentDTO>().ReverseMap();
        }
    }
}
