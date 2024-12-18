using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Identities.User;
using Eshop.Entity.Identities;
using Eshop.Repository.General;


namespace Eshop.Repository.Identities.User
{
    public interface IUserRepository : IBaseRepository<UserEntity>, IScopedDependency
    {
        UserSearchDTO SearchUsers(UserSearchInput req);
        Task<OperationResult<bool>> AddUser(AddUserDTO dto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> EditUser(EditUserDTO userDto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> ChangeUserPassword(ChangeUserPasswordDTO dto, CancellationToken cancellationToken);
        Task<OperationResult<bool>> DeleteUser(string id, CancellationToken cancellationToken);
    }
}
