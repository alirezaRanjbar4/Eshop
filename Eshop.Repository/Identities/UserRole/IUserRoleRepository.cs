using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Identities;
using Eshop.Repository.General;

namespace Eshop.Repository.Identities.UserRole
{
    public interface IUserRoleRepository : IBaseRepository<UserRoleEntity>, IScopedDependency
    {
    }
}
