﻿using AutoMapper;
using Eshop.Application.DTO.Models.Service;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Utilities;

namespace Eshop.Application.Mapping.Models
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<ServiceEntity, ServiceDTO>()
                .ForMember(des => des.ServiceCategoryIds, option => option.MapFrom(src => src.ServiceCategories != null ? src.ServiceCategories.Select(x => x.CategoryId) : null))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null && src.ServicePrices.Any(x => x.ExpiryDate == null) ? src.ServicePrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
                .ReverseMap();

            CreateMap<ServiceEntity, SimpleServiceDTO>()
                .ForMember(des => des.Key, option => option.MapFrom(src => src.Name))
                .ForMember(des => des.Value, option => option.MapFrom(src => src.Id))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null && src.ServicePrices.Any(x => x.ExpiryDate == null) ? src.ServicePrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
                .ReverseMap();

            CreateMap<ServiceEntity, GetServiceDTO>()
               .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null && src.ServicePrices.Any(x => x.ExpiryDate == null) ? src.ServicePrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0))
               .ReverseMap();

            CreateMap<ServiceEntity, GetAllServiceDTO>()
                .ForMember(des => des.Category, option => option.MapFrom(src => src.ServiceCategories != null ? string.Join(",", src.ServiceCategories.Select(x => x.Category.Name)) : string.Empty))
                .ForMember(des => des.Price, option => option.MapFrom(src => src.ServicePrices != null && src.ServicePrices.Any(x => x.ExpiryDate == null) ? src.ServicePrices.OrderByDescending(x => x.CreateDate).First(x => x.ExpiryDate == null).Price : 0));

            CreateMap<ServicePriceEntity, ServicePriceDTO>().ReverseMap();

            CreateMap<ServicePriceEntity, CompleteServicePriceDTO>()
                .ForMember(des => des.ExpiryDate, option => option.MapFrom(src => src.ExpiryDate.HasValue ? Utility.CalandarProvider.MiladiToShamsi(src.ExpiryDate.Value, false) : string.Empty))
                .ForMember(des => des.StartDate, option => option.MapFrom(src => Utility.CalandarProvider.UTCToShamsiWithIranTime(src.CreateDate, false)));
        }
    }
}
