using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.Models.FinancialDocument;
using Eshop.Service.Models.FinancialDocument;
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
    [DisplayName("FinancialDocument")]
    public class FinancialDocumentController : BaseController
    {
        private readonly IFinancialDocumentService _financialDocumentService;
        public FinancialDocumentController(IFinancialDocumentService financialDocumentService)
        {
            _financialDocumentService = financialDocumentService;
        }


        [HttpPost(nameof(GetAllFinancialDocumentWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<GetFinancialDocumentDTO>>> GetAllFinancialDocumentWithTotal(SearchFinancialDocumentDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _financialDocumentService.GetAllAsyncWithTotal<GetFinancialDocumentDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId &&
                (searchDTO.Type == null || x.Type == searchDTO.Type),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddFinancialDocument)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddFinancialDocument([FromBody] AddFinancialDocumentDTO financialDocument, CancellationToken cancellationToken)
        {
            return await _financialDocumentService.AddFinancialDocument(financialDocument, cancellationToken);
        }


        [HttpPost(nameof(UpdateFinancialDocument)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateFinancialDocument([FromBody] AddFinancialDocumentDTO financialDocument, CancellationToken cancellationToken)
        {
            return await _financialDocumentService.UpdateFinancialDocument(financialDocument, cancellationToken);
        }


        [HttpDelete(nameof(DeleteFinancialDocument)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteFinancialDocument(Guid financialDocumentId, CancellationToken cancellationToken)
        {
            return await _financialDocumentService.DeleteAsync(financialDocumentId, true, true, true, cancellationToken);
        }

    }
}
