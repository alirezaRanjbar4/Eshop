using AutoMapper;
using Eshop.Entity.Identities;
using Eshop.Repository.Identities.UserRole;
using Eshop.Service.General;

namespace Eshop.Service.Identity.UserRole
{
    public class UserRoleService : BaseService<UserRoleEntity>, IUserRoleService
    {
        public UserRoleService(IMapper mapper, IUserRoleRepository userRoleRepository) : base(userRoleRepository, mapper)
        {

        }


    }
}
