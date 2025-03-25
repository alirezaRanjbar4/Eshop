using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Models.AccountParty;
using Eshop.DTO.Models.Vendor;
using Eshop.Enum;
using Eshop.Service.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Models
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("AccountParty")]
    public class AccountPartyController : BaseController
    {
        private readonly IAccountPartyService _accountPartyService;
        public AccountPartyController(IAccountPartyService accountPartyService)
        {
            _accountPartyService = accountPartyService;
        }


        [HttpGet(nameof(GetAllAccountParty))]
        public async Task<List<AccountPartyDTO>> GetAllAccountParty(Guid storeId, AccountPartyType type, CancellationToken cancellationToken)
        {
            return await _accountPartyService.GetAllAsync<AccountPartyDTO>(x => x.StoreId == storeId && x.Type == type, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllAccountPartyWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<AccountPartyDTO>>> GetAllAccountPartyWithTotal(AccountPartySearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _accountPartyService.GetAllAsyncWithTotal<AccountPartyDTO>(
            searchDTO,
                x => x.StoreId == searchDTO.Id && x.Type == searchDTO.Type,
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddAccountParty)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddAccountParty([FromBody] AccountPartyDTO accountParty, CancellationToken cancellationToken)
        {
            //var storeId = User.FindFirst("StoreId") != null ? new Guid(User.FindFirst("StoreId").Value) : Guid.Empty;
            var result = await _accountPartyService.AddAsync(accountParty, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateAccountParty)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateAccountParty([FromBody] AccountPartyDTO accountParty, CancellationToken cancellationToken)
        {
            var result = await _accountPartyService.UpdateAsync(accountParty, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteAccountParty)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteAccountParty(Guid accountPartyId, CancellationToken cancellationToken)
        {
            return await _accountPartyService.DeleteAsync(accountPartyId, true, true, true, cancellationToken);
        }

    }
}
