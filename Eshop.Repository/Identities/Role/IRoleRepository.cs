using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Identities;
using Eshop.Repository.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Repository.Identities.Role
{
    public interface IRoleRepository : IBaseRepository<RoleEntity>, IScopedDependency
    {
    }
}
