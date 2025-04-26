using Eshop.Application.DTO.Models.Store;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
{
    [Authorize]
    [DisplayName("Store")]
    public class StoreController : BaseController
    {
        private readonly IBaseService<StoreEntity> _storeService;
        public StoreController(IBaseService<StoreEntity> storeService)
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
