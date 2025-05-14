using AutoMapper;
using Eshop.Application.DTO.Models.AdditionalCost;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Utilities;

namespace Eshop.Application.Mapping.Models
{
    public class AdditionalCostProfile : Profile
    {
        public AdditionalCostProfile()
        {
            CreateMap<AdditionalCostEntity, AdditionalCostDTO>()
                .ForMember(des => des.String_Date, option => option.MapFrom(src => Utility.CalandarProvider.MiladiToShamsi(src.Date, false)))
                .ReverseMap();
        }
    }
}
