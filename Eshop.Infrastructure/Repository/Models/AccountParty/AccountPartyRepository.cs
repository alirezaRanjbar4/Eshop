using Eshop.Domain.Models;
using Eshop.Infrastructure.DBContext;
using Eshop.Infrastructure.Repository.General;

namespace Eshop.Infrastructure.Repository.Models.AccountParty
{
    public class AccountPartyRepository : BaseRepository<AccountPartyEntity>, IAccountPartyRepository
    {
        public AccountPartyRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
