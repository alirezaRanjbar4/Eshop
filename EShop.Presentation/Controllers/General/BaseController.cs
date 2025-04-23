using Eshop.Share.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Eshop.Presentation.Controllers.General
{
    [Route($"api/v{{version:apiVersion}}/[controller]")]
    [ApiController]
    [ApiResultFilter]
    [ServiceFilter(typeof(UserActionFilter))]
    public class BaseController : ControllerBase
    {
        protected Guid CurrentUserStoreId
        {
            get
            {
                var storeIdClaim = User.FindFirst("StoreId");
                return storeIdClaim != null && !string.IsNullOrEmpty(storeIdClaim.Value) && Guid.TryParse(storeIdClaim.Value, out var storeId)
                    ? storeId
                    : Guid.Empty;
            }
        }
    }
}
