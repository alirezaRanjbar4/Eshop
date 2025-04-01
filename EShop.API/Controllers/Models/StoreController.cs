using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.Enum;
using Eshop.DTO.Models.Store;
using Eshop.Service.Models.Store;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Models
{
    [ApiVersion(VersionProperties.V1)]
    [Authorize]
    [DisplayName("Store")]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }


        [HttpGet(nameof(GetStoreDetail))]
        public async Task<StoreDTO> GetStoreDetail(CancellationToken cancellationToken)
        {
            return await _storeService.GetAsync<StoreDTO>(x => x.Id == CurrentUserStoreId, null, false, cancellationToken);
        }


        [HttpPost(nameof(UpdateStore)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateStore([FromBody] LimitedStoreDTO store, CancellationToken cancellationToken)
        {
            var result = await _storeService.UpdateAsync(store, true, true, cancellationToken);
            return result != null;
        }

    }
}
