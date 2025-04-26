using Eshop.Domain.Identities;
using Eshop.Infrastructure.Repository.General;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Infrastructure.Repository.Identities.User
{
    public interface IUserRepository : IBaseRepository<UserEntity>, IScopedDependency
    {

    }
}
