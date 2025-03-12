using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Identities;
using Eshop.Repository.General;

namespace Eshop.Repository.Identities.Role
{
    public interface IRoleRepository : IBaseRepository<RoleEntity>, IScopedDependency
    {
    }
}
