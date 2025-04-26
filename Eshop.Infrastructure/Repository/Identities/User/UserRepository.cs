using Eshop.Domain.Identities;
using Eshop.Infrastructure.DBContext;
using Eshop.Infrastructure.Repository.General;

namespace Eshop.Infrastructure.Repository.Identities.User
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}