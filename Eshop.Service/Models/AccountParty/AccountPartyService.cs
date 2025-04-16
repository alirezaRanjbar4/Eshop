using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Customer;
using Eshop.Service.General;

namespace Eshop.Service.Models.AccountParty
{
    public class AccountPartyService : BaseService<AccountPartyEntity>, IAccountPartyService
    {
        //private readonly IUserService _userService;
        //private readonly IRoleService _roleService;
        //private readonly IUserRoleService _userRoleService;
        public AccountPartyService(
            IMapper mapper,
            IAccountPartyRepository AccountPartyRepository
            //IRoleService roleService,
            //IUserService userService,
            //IUserRoleService userRoleService
            ) : base(AccountPartyRepository, mapper)
        {
            //_userService = userService;
            //_roleService = roleService;
            //_userRoleService = userRoleService;
        }

        //public async Task<bool> AddAccountParty(AccountPartyUserDTO customerUser, CancellationToken cancellationToken)
        //{
        //    var user = _mapper.Map<AddUserDTO>(customerUser);
        //    var addUserResult = await _userService.AddUser(user, cancellationToken);
        //    if (!addUserResult.Data)
        //        return false;

        //    var customer = _mapper.Map<AccountPartyDTO>(customerUser);
        //    customer.UserId = user.Id;
        //    customer.Id = Guid.Empty;
        //    var addAccountPartyresult = await AddAsync(customer, true, cancellationToken);
        //    if (addAccountPartyresult == null)
        //        return false;

        //    var customerRole = await _roleService.GetRoleByNameAsync("AccountParty", cancellationToken);
        //    var userRole = new UserRoleDTO
        //    {
        //        RoleId = customerRole.Id,
        //        UserId = user.Id
        //    };
        //    var addUserRoleResult = await _userRoleService.AddAsync(userRole, true, cancellationToken);

        //    return addUserRoleResult != null;
        //}

        //public async Task<bool> UpdateAccountParty(AccountPartyUserDTO customerUser, CancellationToken cancellationToken)
        //{
        //    var user = _mapper.Map<EditUserDTO>(customerUser);
        //    user.Id = customerUser.UserId;
        //    var editUserResult = await _userService.EditUser(user, cancellationToken);
        //    if (!editUserResult.Data)
        //        return false;

        //    var customer = _mapper.Map<AccountPartyDTO>(customerUser);
        //    var result = await UpdateAsync(customer, true, true, cancellationToken);
        //    return result != null;
        //}
    }
}
