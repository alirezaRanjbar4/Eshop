using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.Core;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.Category;
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
    [DisplayName("Category")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _tramTypeService;
        public CategoryController(ICategoryService tramTypeService)
        {
            _tramTypeService = tramTypeService;
        }


        [HttpGet(nameof(GetAllCategory))]
        public async Task<List<CategoryDTO>> GetAllCategory(CancellationToken cancellationToken)
        {
            return await _tramTypeService.GetAllAsync<CategoryDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllCategoryWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<CategoryDTO>>> GetAllCategoryWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _tramTypeService.GetAllAsyncWithTotal<CategoryDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddCategory)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddCategory([FromBody] CategoryDTO tramType, CancellationToken cancellationToken)
        {
            var result = await _tramTypeService.AddAsync(tramType, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateCategory)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateCategory([FromBody] CategoryDTO tramType, CancellationToken cancellationToken)
        {
            var result = await _tramTypeService.UpdateAsync(tramType, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteCategory)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteCategory(Guid tramTypeId, CancellationToken cancellationToken)
        {
            return await _tramTypeService.DeleteAsync(tramTypeId, true, true, true, cancellationToken);
        }

    }
}
