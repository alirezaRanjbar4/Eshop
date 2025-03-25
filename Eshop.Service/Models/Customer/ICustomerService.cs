using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Customer
{
    public interface IAccountPartyService : IBaseService<AccountPartyEntity>, IScopedDependency
    {
        //Task<bool> AddAccountParty(AccountPartyUserDTO customerUser, CancellationToken cancellationToken);
        //Task<bool> UpdateAccountParty(AccountPartyUserDTO customerUser, CancellationToken cancellationToken);
    }
}
