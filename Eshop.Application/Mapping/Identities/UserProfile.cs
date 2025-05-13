using AutoMapper;
using Eshop.Application.DTO.Identities.Authentication;
using Eshop.Application.DTO.Identities.DynamicAccess;
using Eshop.Application.DTO.Identities.Role;
using Eshop.Application.DTO.Identities.User;
using Eshop.Domain.Identities;
using Eshop.Share.Enum;
using System.Security.Claims;

namespace Eshop.Application.Mapping.Identities
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // make sure all datetime values are UTC
            ValueTransformers.Add<DateTime>(val => !((DateTime?)val).HasValue ? val : DateTime.SpecifyKind(val, DateTimeKind.Utc));
            ValueTransformers.Add<DateTime?>(val => val.HasValue ? DateTime.SpecifyKind(val.Value, DateTimeKind.Utc) : val);

            CreateMap<UserEntity, UserDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserEntity, UserSimpleDTO>();

            CreateMap<UserDTO, UserEntity>()
                .ForMember(dest => dest.UserRoles, opt => opt.MapFrom(src => src.UserRoles))
                .ForMember(dest => dest.Claims, opt => opt.MapFrom(src => src.Claims));

            CreateMap<RoleClaimEntity, RoleClaimDto>()
               .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.ClaimType))
               .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.ClaimValue))
               .ReverseMap();

            CreateMap<AddUserDTO, UserEntity>().ReverseMap();

            CreateMap<UserEntity, EditUserDTO>();

            CreateMap<EditUserDTO, UserEntity>();

            CreateMap<UserRoleEntity, UserRoleDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleClaims, opt => opt.MapFrom(src => src.Role.RoleClaims))
                .ForMember(dest => dest.RoleNormalizedName, opt => opt.MapFrom(src => src.Role.NormalizedName));

            CreateMap<UserRoleEntity, LoginUserRolesDTO>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));


            CreateMap<UserRoleDTO, UserRoleEntity>();

            CreateMap<RoleEntity, RoleDTO>();
            // CreateMap<RoleEntity, SimpleRoleDTO>();

            CreateMap<RoleEntity, GetRoleDTO>();
            CreateMap<RoleDTO, RoleEntity>();
            CreateMap<AddRoleDTO, RoleEntity>();
            CreateMap<RoleEntity, BaseRoleDTO>();
            CreateMap<RoleEntity, SimpleRoleDTO>();

            CreateMap<Claim, ClaimDTO>();


            CreateMap<UserEntity, LoginDTO>()
                .ForMember(dest => dest.Claims, opt => opt.Ignore())
                .AfterMap((src, des) =>
                {
                    switch (src.UserType)
                    {
                        case UserType.Admin:
                            des.Name = src.UserName;
                            break;
                        case UserType.Vendor:
                            des.Name = src.Vendor != null ? src.Vendor.Name : string.Empty;
                            des.StoreName = src.Vendor != null && src.Vendor.Store != null ? src.Vendor.Store.Name : string.Empty;
                            des.StoreType = src.Vendor != null && src.Vendor.Store != null ? src.Vendor.Store.StoreType : null;
                            des.StoreId = src.Vendor != null ? src.Vendor.StoreId : null;
                            break;
                        case UserType.AccountParty:
                            des.Name = src.AccountParty.Name;
                            break;
                        default:
                            break;
                    }
                    if (src != null && src.UserRoles != null && src.UserRoles.Count > 0)
                    {
                        List<ActionDetailsDTO> actionDetails = new List<ActionDetailsDTO>();
                        List<ControllerDTO> controllers = new List<ControllerDTO>();

                        for (int i = 0; i < src.UserRoles.Count; i++)
                        {
                            var userRole = src.UserRoles.ElementAt(i);
                            if (userRole != null && userRole.Role.RoleClaims.Any(x => !x.Deleted))
                            {
                                var roleClaims = userRole.Role.RoleClaims.Where(x => !x.Deleted)
                                                                         .Where(x => x.ClaimValue.StartsWith(":")) // انتخاب تمامی موارد که با ":" شروع می‌شوند
                                                                         .Select(x => x.ClaimValue.Substring(1).Split(':')[0]) // استخراج بخش مشترک برای گروه‌بندی
                                                                         .Distinct() // حذف موارد تکراری
                                                                         .ToList();

                                for (int j = 0; j < roleClaims.Count(); j++)
                                {
                                    var roleClaim = roleClaims.ElementAt(j);

                                    var findRoleClaim = userRole.Role.RoleClaims.Where(x => x.ClaimValue.Split(':')[1].Equals(roleClaim)).ToList();
                                    if (findRoleClaim.Any())
                                    {
                                        actionDetails = new List<ActionDetailsDTO>();
                                        for (int o = 0; o < findRoleClaim.Count; o++)
                                        {
                                            actionDetails.Add(new ActionDetailsDTO
                                            {
                                                ActionName = findRoleClaim.ElementAt(o).ClaimValue,
                                                ActionDisplayName = string.Empty,
                                                ActionId = findRoleClaim.ElementAt(o).ClaimValue,
                                                ControllerId = findRoleClaim.ElementAt(o).ClaimValue.Split(":")[1],
                                            });
                                        }
                                    }

                                    ControllerDTO controller = new ControllerDTO
                                    {
                                        AreaName = string.Empty,
                                        ControllerId = roleClaim,
                                        ControllerName = roleClaim,
                                        ControllerDisplayName = string.Empty,
                                        ActionDetails = actionDetails
                                    };

                                    if (!controllers.Any(x => x.ControllerId == controller.ControllerId))
                                    {
                                        controllers.Add(controller);
                                    }
                                }
                            }
                        }

                        des.Claims = des.Claims ?? new List<ControllerDTO>();
                        des.Claims = controllers;
                    }
                });

        }
    }
}
