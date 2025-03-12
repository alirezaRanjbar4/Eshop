using AutoMapper;
using Eshop.DTO.Identities.User;
using Eshop.DTO.Models.Customer;
using Eshop.DTO.Models.Vendor;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Customer;
using Eshop.Service.General;
using Eshop.Service.Identity.Role;
using Eshop.Service.Identity.User;
using Eshop.Service.Identity.UserRole;

namespace Eshop.Service.Models.Customer
{
    public class CustomerService : BaseService<CustomerEntity>, ICustomerService
    {
        //private readonly IUserService _userService;
        //private readonly IRoleService _roleService;
        //private readonly IUserRoleService _userRoleService;
        public CustomerService(
            IMapper mapper,
            ICustomerRepository CustomerRepository
            //IRoleService roleService,
            //IUserService userService,
            //IUserRoleService userRoleService
            ) : base(CustomerRepository, mapper)
        {
            //_userService = userService;
            //_roleService = roleService;
            //_userRoleService = userRoleService;
        }

        //public async Task<bool> AddCustomer(CustomerUserDTO customerUser, CancellationToken cancellationToken)
        //{
        //    var user = _mapper.Map<AddUserDTO>(customerUser);
        //    var addUserResult = await _userService.AddUser(user, cancellationToken);
        //    if (!addUserResult.Data)
        //        return false;

        //    var customer = _mapper.Map<CustomerDTO>(customerUser);
        //    customer.UserId = user.Id;
        //    customer.Id = Guid.Empty;
        //    var addCustomerresult = await AddAsync(customer, true, cancellationToken);
        //    if (addCustomerresult == null)
        //        return false;

        //    var customerRole = await _roleService.GetRoleByNameAsync("Customer", cancellationToken);
        //    var userRole = new UserRoleDTO
        //    {
        //        RoleId = customerRole.Id,
        //        UserId = user.Id
        //    };
        //    var addUserRoleResult = await _userRoleService.AddAsync(userRole, true, cancellationToken);

        //    return addUserRoleResult != null;
        //}

        //public async Task<bool> UpdateCustomer(CustomerUserDTO customerUser, CancellationToken cancellationToken)
        //{
        //    var user = _mapper.Map<EditUserDTO>(customerUser);
        //    user.Id = customerUser.UserId;
        //    var editUserResult = await _userService.EditUser(user, cancellationToken);
        //    if (!editUserResult.Data)
        //        return false;

        //    var customer = _mapper.Map<CustomerDTO>(customerUser);
        //    var result = await UpdateAsync(customer, true, true, cancellationToken);
        //    return result != null;
        //}
    }
}
