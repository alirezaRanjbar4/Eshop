using AutoMapper;
using Eshop.Application.DTO.Models.SchedulerTask;
using Eshop.Domain.Models;

namespace Eshop.Application.Mapping.Models
{
    public class SchedulerTaskProfile : Profile
    {
        public SchedulerTaskProfile()
        {
            CreateMap<SchedulerTaskVendorEntity, SchedulerTaskVendorDTO>().ReverseMap();

            CreateMap<SchedulerTaskVendorEntity, SchedulerTaskVendorReminderDTO>()
                .ForMember(des => des.Title, opt => opt.MapFrom(src => src.SchedulerTask != null ? src.SchedulerTask.Title : string.Empty))
                .ForMember(des => des.UserId, opt => opt.MapFrom(src => src.Vendor != null && src.Vendor.User != null ? src.Vendor.User.Id : Guid.Empty));

            CreateMap<EditSchedulerTaskVendorTimeDTO, SchedulerTaskVendorEntity>();

            CreateMap<SchedulerTaskVendorEntity, CalenderSchedulerItemVM>()
                .ForMember(des => des.Title, opt => opt.MapFrom(src => src.SchedulerTask != null ? src.SchedulerTask.Title : string.Empty))
                .ForMember(des => des.TaskDescription, opt => opt.MapFrom(src => src.SchedulerTask != null ? src.SchedulerTask.Description : string.Empty))
                .ForMember(des => des.Time, opt => opt.MapFrom(src => src.Time.ToString("HH:mm")))
                .ForMember(des => des.Name, opt => opt.MapFrom(src => src.Vendor != null ? src.Vendor.Name : string.Empty));

            CreateMap<SchedulerTaskEntity, SchedulerTaskDTO>()
               .ForMember(des => des.Vendors, opt => opt.MapFrom(src => src.AssignedTo.Select(x => x.VendorId).ToList()));

            CreateMap<SchedulerTaskDTO, SchedulerTaskEntity>();
        }
    }
}
