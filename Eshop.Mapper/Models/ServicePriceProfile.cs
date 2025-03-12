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
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => src.ExpiryDate.HasValue ? Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate.Value, false) : string.Empty))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));
        }
    }
}
