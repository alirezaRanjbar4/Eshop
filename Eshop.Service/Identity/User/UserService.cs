using AutoMapper;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.Common.Exceptions;
using Eshop.DTO.Identities.User;
using Eshop.Entity.Identities;
using Eshop.Repository.Identities.User;
using Eshop.Service.General;

namespace Eshop.Service.Identity.User
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository, IMapper mapper) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
        }

        public UserSearchDTO SearchUsers(UserSearchInput req)
        {
            return _userRepository.SearchUsers(req);
        }

        public async Task<OperationResult<bool>> AddUser(AddUserDTO dto, CancellationToken cancellationToken)
        {
            return await _userRepository.AddUser(dto, cancellationToken);
        }

        public async Task<OperationResult<bool>> EditUser(AddUserDTO dto, CancellationToken cancellationToken)
        {
            return await _userRepository.EditUser(dto, cancellationToken);
        }

        public async Task<OperationResult<bool>> ChangeUserPassword(ChangeUserPasswordDTO dto, CancellationToken cancellationToken)
        {
            UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

            var result = await _userRepository.ChangeUserPassword(dto, cancellationToken);
            if (result != null && !result.Data && result.Errors.Any())
            {
                foreach (var item in result.Errors)
                {
                    switch (item.Code)
                    {
                        case "DuplicateUserName":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.DuplicateUserName);
                            break;
                        case "PasswordRequiresNonAlphanumeric":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordRequiresNonAlphanumeric);
                            break;
                        case "PasswordRequiresDigit":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordRequiresDigit);
                            break;
                        case "PasswordRequiresLower":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordRequiresLower);
                            break;
                        case "PasswordRequiresUpper":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordRequiresUpper);
                            break;
                        case "PasswordTooShort":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordTooShort);
                            break;
                        case "InvalidUserName":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.InvalidUserName);
                            break;
                        case "DuplicateEmail":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.DuplicateEmail);
                            break;
                        case "DuplicateRoleName":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.DuplicateRoleName);
                            break;
                        case "PasswordMismatch":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordMismatch);
                            break;
                        case "IncorrectPassword":
                            validationExceptions.OperationState.ResourceKeyList.Add(Common.Enum.User.PasswordMismatch);
                            break;

                    }
                }
                if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
                    throw validationExceptions;
            }
            return result;
        }

        public async Task<OperationResult<bool>> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteUser(id.ToString(), cancellationToken);
        }

    }
}