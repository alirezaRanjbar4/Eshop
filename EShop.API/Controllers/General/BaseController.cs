using Eshop.Common.ActionFilters;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Api.Controllers.General
{
    [Route($"api/v{{version:apiVersion}}/[controller]")]
    [ApiController]
    [ApiResultFilter]
    [ServiceFilter(typeof(UserActionFilter))]
    public class BaseController : ControllerBase
    {
        public const string AdministratorUser = "ADMINISTRATOR";
    }
}
