using AutoMapper;
using Eshop.Application.DTO.Identities.User;
using Eshop.Application.Service.General;
using Eshop.Domain.Identities;
using Eshop.Infrastructure.Repository.Identities.User;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Eshop.Share.Exceptions;
using Eshop.Share.Extensions;
using Eshop.Share.Helpers.Utilities.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Service.Identity.User
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        private readonly UserManager<UserEntity> _userManager;
        public UserService(
            UserManager<UserEntity> userManager,
            IUserRepository userRepository,
            IMapper mapper) : base(userRepository, mapper)
        {
            _userManager = userManager;
        }

        public async Task<OperationResult<bool>> AddUser(AddUserDTO dto, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>() { Data = true };
            try
            {
                var user = _mapper.Map<UserEntity>(dto);
                user.PhoneNumber = user.PhoneNumber.ToPhone();
                user.Activated = true;

                if (string.IsNullOrEmpty(dto.PassWord))
                    dto.PassWord = Utility.SecurityHelper.GeneratePassword();

                var addResult = await _userManager.CreateAsync(user, dto.PassWord);
                if (!addResult.Succeeded)
                {
                    result.Data = false;
                    result.Message = string.Join(",", TranslateIdentityErrors(addResult.Errors));
                    return result;
                }
                ;
                dto.Id = user.Id;

                return result;
            }
            catch (Exception ex)
            {
                result.Data = false;
                result.Message = ex.ToString();
                return result;
            }
        }

        public async Task<OperationResult<bool>> EditUser(AddUserDTO dto, CancellationToken cancellationToken)
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

                if (!string.IsNullOrEmpty(dto.PassWord))
                {
                    var removePassResult = await _userManager.RemovePasswordAsync(user);
                    if (!removePassResult.Succeeded)
                    {
                        return new OperationResult<bool>()
                        {
                            Data = false,
                            Message = string.Join("***", removePassResult.Errors.Select(x => x.Description))
                        };
                    }

                    var addPassResult = await _userManager.AddPasswordAsync(user, dto.PassWord);
                    if (!addPassResult.Succeeded)
                    {
                        return new OperationResult<bool>()
                        {
                            Data = false,
                            Message = string.Join("***", addPassResult.Errors.Select(x => x.Description))
                        };
                    }
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
            UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

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

                if (result != null && !result.Succeeded && result.Errors.Any())
                {
                    foreach (var item in result.Errors)
                    {
                        switch (item.Code)
                        {
                            case "DuplicateUserName":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.DuplicateUserName);
                                break;
                            case "PasswordRequiresNonAlphanumeric":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordRequiresNonAlphanumeric);
                                break;
                            case "PasswordRequiresDigit":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordRequiresDigit);
                                break;
                            case "PasswordRequiresLower":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordRequiresLower);
                                break;
                            case "PasswordRequiresUpper":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordRequiresUpper);
                                break;
                            case "PasswordTooShort":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordTooShort);
                                break;
                            case "InvalidUserName":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.InvalidUserName);
                                break;
                            case "DuplicateEmail":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.DuplicateEmail);
                                break;
                            case "DuplicateRoleName":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.DuplicateRoleName);
                                break;
                            case "PasswordMismatch":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordMismatch);
                                break;
                            case "IncorrectPassword":
                                validationExceptions.OperationState.ResourceKeyList.Add(Share.Enum.User.PasswordMismatch);
                                break;

                        }
                    }

                    if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
                        throw validationExceptions;
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

    }
}