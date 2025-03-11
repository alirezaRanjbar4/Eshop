using AutoMapper;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Models.Service;
using Eshop.Entity.Models;

namespace Eshop.Mapper.Models
{
    public class ServicePriceProfile : Profile
    {
        public ServicePriceProfile()
        {
            CreateMap<ServicePriceEntity, ServicePriceDTO>().ReverseMap();

            CreateMap<ServicePriceEntity, CompleteServicePriceDTO>()
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate, false)))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));
        }
    }
}
