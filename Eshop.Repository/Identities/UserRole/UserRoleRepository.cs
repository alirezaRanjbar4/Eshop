using Eshop.Entity.Identities;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Identities.UserRole
{
    public class UserRoleRepository : BaseRepository<UserRoleEntity>, IUserRoleRepository
    {
        public UserRoleRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
