using AutoMapper;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Extensions;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.DTO.Identities.User;
using Eshop.Entity.Identities;
using Eshop.Repository.General;
using Eshop.Repository.Identities.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Identities.User
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private UserManager<UserEntity> _userManager;
        private IMapper _mapper { get; set; }

        public UserRepository(
            ApplicationContext dataContext,
            UserManager<UserEntity> userManager,
            IMapper mapper) : base(dataContext)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public UserSearchDTO SearchUsers(UserSearchInput requsetUserDto)
        {
            // define the return object
            var result = new UserSearchDTO();
            result.Page = result.Page == 0 ? 1 : result.Page;
            result.PageSize = result.PageSize == 0 ? 20 : result.PageSize;

            // ensure some values
            if (string.IsNullOrEmpty(requsetUserDto.sort))
                requsetUserDto.sort = "lastName";

            // query the users            
            var list = dbSet.Include(o => o.UserRoles).ThenInclude(o => o.Role).AsQueryable();
            //.Where(o => o.Id != null);

            if (!string.IsNullOrEmpty(requsetUserDto.keyword))
            {
                requsetUserDto.keyword = requsetUserDto.keyword.ToLower();

                list = list.Where(o =>
                    o.UserName.ToLower().Contains(requsetUserDto.keyword) ||
                    o.Email.ToLower().Contains(requsetUserDto.keyword)
                );
            }

            if (requsetUserDto.roleid != Guid.Empty)
                list = list.Where(o => o.UserRoles.Any(r => r.RoleId == requsetUserDto.roleid));

            switch (requsetUserDto.sort.ToLower())
            {
                case "username":
                    if (requsetUserDto.desc)
                        list = list.OrderByDescending(o => o.UserName);
                    else
                        list = list.OrderBy(o => o.UserName);
                    break;
                //case "firstname":
                //    if (requsetUserDto.desc)
                //        list = list.OrderByDescending(o => o.Person.FirstName);
                //    else
                //        list = list.OrderBy(o => o.Person.FirstName);
                //    break;
                case "email":
                    if (requsetUserDto.desc)
                        list = list.OrderByDescending(o => o.Email);
                    else
                        list = list.OrderBy(o => o.Email);
                    break;
                    //default: // lastname
                    //if (requsetUserDto.desc)
                    //    list = list.OrderByDescending(o => o.Person.LastName);
                    //else
                    //    list = list.OrderBy(o => o.Person.LastName);
                    //break;
            }

            var all = list.Skip((result.Page - 1) * result.PageSize).Take(result.PageSize).ToList();

            // convert to DTOs
            result.Users = _mapper.Map<IEnumerable<UserDTO>>(all).ToList();

            // get the count from another query
            var u = dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(requsetUserDto.keyword))
            {
                requsetUserDto.keyword = requsetUserDto.keyword.ToLower();

                u = u.Where(o =>
                    o.Email.ToLower().Contains(requsetUserDto.keyword)
                );
            }

            if (requsetUserDto.roleid != Guid.Empty)
                u = u.Where(o => o.UserRoles.Any(r => r.RoleId == requsetUserDto.roleid));

            result.TotalRecords = u.Count();

            return result;
        }

        public async Task<OperationResult<bool>> AddUser(AddUserDTO userDto, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>() { Data = true };
            try
            {
                var user = _mapper.Map<UserEntity>(userDto);
                user.PhoneNumber = user.PhoneNumber.ToPhone();

                if (string.IsNullOrEmpty(userDto.PassWord))
                    userDto.PassWord = Utility.SecurityHelper.GeneratePassword();

                var addResult = await _userManager.CreateAsync(user, userDto.PassWord);
                if (!addResult.Succeeded)
                {
                    result.Data = false;
                    result.Message = string.Join(",", TranslateIdentityErrors(addResult.Errors));
                    return result;
                };

                var saveResult = await SaveChangesAsync(cancellationToken);
                if (saveResult <= 0)
                {
                    result.Data = false;
                    result.Message = "خطا سرور";
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.Message = ex.ToString();
                return result;
            }
        }

        private IEnumerable<string> TranslateIdentityErrors(IEnumerable<IdentityError> Errors)
        {
            List<string> stringList = new List<string>();
            foreach (var item in Errors)
            {
                switch (item.Code)
                {
                    case "DuplicateUserName":
                        stringList.Add($"نام کاربری  قبلا توسط شخص دیگری انتخاب شده است.");
                        break;
                    case "PasswordRequiresNonAlphanumeric":
                        stringList.Add("کلمه عبور باید حداقل شامل یک کاراکتر غیرعددی و غیر حرفی باشد.(@,%,#,...)");
                        break;
                    case "PasswordRequiresDigit":
                        stringList.Add("کلمه عبور باید حداقل شامل یک عدد (0-9) باشد.");
                        break;
                    case "PasswordRequiresLower":
                        stringList.Add("کلمه عبور باید حداقل شامل یک حرف  کوچک (a-z) باشد.");
                        break;
                    case "PasswordRequiresUpper":
                        stringList.Add("کلمه عبور باید حداقل شامل یک حرف بزرگ (A-Z) باشد.");
                        break;
                    case "PasswordTooShort":
                        stringList.Add("کلمه عبور باید حداقل شامل کاراکتر باشد.");
                        break;
                    case "InvalidUserName":
                        stringList.Add("نام کاربری باید شامل کاراکترهای (0-9) و (a-z) باشد.");
                        break;
                    case "DuplicateEmail":
                        stringList.Add("شما با ایمیل قبلا ثبت نام کرده اید.");
                        break;
                    case "DuplicateRoleName":
                        stringList.Add("نقش تکراری است");
                        break;
                    case "PasswordMismatch":
                        stringList.Add("کلمه عبور فعلی شما صحیح نمی باشد");
                        break;

                }
            }
            return stringList;
        }

        public async Task<OperationResult<bool>> EditUser(EditUserDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(o => o.Id == dto.Id);
                if (user == null)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = "نام کاربری یافت نشد"
                    };
                }

                var userNameResult = await _userManager.SetUserNameAsync(user, dto.UserName);
                if (!userNameResult.Succeeded)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = string.Join("***", userNameResult.Errors.Select(x => x.Description))
                    };
                }

                var mapResult = _mapper.Map(dto, user);
                var editResult = await _userManager.UpdateAsync(mapResult);
                if (!editResult.Succeeded)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = string.Join("***", editResult.Errors.Select(x => x.Description))
                    };
                }

                return new OperationResult<bool>()
                {
                    Data = true,
                    Message = "ویرایش اطلاعات کاربری با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>()
                {
                    Data = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<OperationResult<bool>> ChangeUserPassword(ChangeUserPasswordDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(o => o.Id == dto.Id, cancellationToken);
                if (user == null)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = "نام کاربری یافت نشد"
                    };
                }

                var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
                if (!result.Succeeded)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = string.Join("***", result.Errors.Select(x => x.Description)),
                        Errors = result.Errors
                    };
                }

                return new OperationResult<bool>()
                {
                    Data = true,
                    Message = "تغییر کلمه عبور با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>()
                {
                    Data = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<OperationResult<bool>> DeleteUser(string id, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = "نام کاربری یافت نشد"
                    };
                }

                user.Deleted = true;
                dbSet.Update(user);
                var saveResult = await SaveChangesAsync(cancellationToken);

                if (saveResult<=0)
                {
                    return new OperationResult<bool>()
                    {
                        Data = false,
                        Message = "خطا سرور"
                    };
                }

                return new OperationResult<bool>()
                {
                    Data = true,
                    Message = "حذف کاربر با موفقیت انجام شد"
                };
            }
            catch (Exception ex)
            {
                return new OperationResult<bool>()
                {
                    Data = false,
                    Message = ex.Message
                };
            }
            
        }
    }
}