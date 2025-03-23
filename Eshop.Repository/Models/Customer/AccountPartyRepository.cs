using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.AccountParty
{
    public class AccountPartyRepository : BaseRepository<AccountPartyEntity>, IAccountPartyRepository
    {
        public AccountPartyRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
