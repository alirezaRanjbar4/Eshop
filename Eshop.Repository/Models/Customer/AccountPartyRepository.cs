using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Customer
{
    public class AccountPartyRepository : BaseRepository<AccountPartyEntity>, IAccountPartyRepository
    {
        public AccountPartyRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
