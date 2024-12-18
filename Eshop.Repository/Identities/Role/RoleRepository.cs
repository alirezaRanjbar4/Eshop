using Eshop.Entity.Identities;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Identities.Role
{
    public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
    {
        public RoleRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
