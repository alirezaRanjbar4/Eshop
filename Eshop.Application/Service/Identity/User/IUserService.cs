using Eshop.Application.DTO.Identities.User;
using Eshop.Application.Service.General;
using Eshop.Domain.Identities;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Identity.User
{
    public interface IUserService : IBaseService<UserEntity>, IScopedDependency
    {
        Task<OperationResult<bool>> AddUser(AddUserDTO userDto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> EditUser(AddUserDTO userDto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> ChangeUserPassword(ChangeUserPasswordDTO dto, CancellationToken cancellationToken);
    }
}
