using Asp.Versioning;
using Eshop.Application.DTO.Models.DemoRequest;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Presentation.Components;
using Eshop.Presentation.Controllers.General;
using Eshop.Share.ActionFilters;
using Eshop.Share.Enum;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Presentation.Controllers.Models
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
