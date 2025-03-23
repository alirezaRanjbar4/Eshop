using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.AccountParty
{
    public interface IAccountPartyRepository : IBaseRepository<AccountPartyEntity>, IScopedDependency
    {
    }
}
