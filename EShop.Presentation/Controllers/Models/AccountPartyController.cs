using Asp.Versioning;
using Eshop.Application.DTO.General;
using Eshop.Application.DTO.Models.AccountParty;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Presentation.Components;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.ActionFilters.Response;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("AccountParty")]
    public class AccountPartyController : BaseController
    {
        private readonly IBaseService<AccountPartyEntity> _accountPartyService;
        public AccountPartyController(IBaseService<AccountPartyEntity> accountPartyService)
        {
            _accountPartyService = accountPartyService;
        }


        [HttpGet(nameof(GetAllAccountParty))]
        public async Task<List<SimpleDTO>> GetAllAccountParty(AccountPartyType type, CancellationToken cancellationToken)
        {
            return await _accountPartyService.GetAllAsync<SimpleDTO>(x => x.StoreId == CurrentUserStoreId && x.Type == type, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllAccountPartyWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<AccountPartyDTO>>> GetAllAccountPartyWithTotal([FromBody] AccountPartySearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _accountPartyService.GetAllAsyncWithTotal<AccountPartyDTO>(
            searchDTO,
                x => x.StoreId == CurrentUserStoreId && (searchDTO.Type == null || x.Type == searchDTO.Type),
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
