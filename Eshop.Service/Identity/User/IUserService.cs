using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Identities.User;
using Eshop.Entity.Identities;
using Eshop.Service.General;

namespace Eshop.Service.Identity.User
{
    public interface IUserService : IBaseService<UserEntity>, IScopedDependency
    {
        UserSearchDTO SearchUsers(UserSearchInput req);
        Task<OperationResult<bool>> AddUser(AddUserDTO userDto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> EditUser(AddUserDTO userDto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> ChangeUserPassword(ChangeUserPasswordDTO dto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteUser(Guid id, CancellationToken cancellationToken);
    }
}
