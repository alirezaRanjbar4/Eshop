using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Infrastructure.Repository.Models.AccountParty
{
    public interface IAccountPartyRepository : IBaseRepository<AccountPartyEntity>, IScopedDependency
    {
    }
}
