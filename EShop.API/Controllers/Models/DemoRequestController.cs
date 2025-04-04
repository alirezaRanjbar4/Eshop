using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.Enum;
using Eshop.DTO.Models.DemoRequest;
using Eshop.Entity.Models;
using Eshop.Service.General;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Controllers.Models
{
    [ApiVersion(VersionProperties.V1)]
    [DisplayName("DemoRequest")]
    public class DemoRequestController : BaseController
    {
        private readonly IBaseService<DemoRequestEntity> _demoRequestService;
        public DemoRequestController(IBaseService<DemoRequestEntity> demoRequestService)
        {
            _demoRequestService = demoRequestService;
        }


        [HttpPost(nameof(AddDemoRequest))]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddDemoRequest([FromBody] LimitedDemoRequestDTO demoRequest, CancellationToken cancellationToken)
        {
            var result = await _demoRequestService.AddAsync(demoRequest, true, cancellationToken);
            return result != null;
        }

    }
}
