using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.Models.Category;
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
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet(nameof(GetAllCategory))]
        public async Task<List<CategoryDTO>> GetAllCategory(CategoryType type, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllAsync<CategoryDTO>(x => x.StoreId == CurrentUserStoreId && x.Type == type, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllCategoryWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<CategoryDTO>>> GetAllCategoryWithTotal([FromBody] SearchCategoryDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _categoryService.GetAllAsyncWithTotal<CategoryDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId &&
                (searchDTO.Type == null || x.Type == searchDTO.Type) &&
                (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm)),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddCategory)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddCategory([FromBody] CategoryDTO category, CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddAsync(category, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateCategory)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateCategory([FromBody] CategoryDTO category, CancellationToken cancellationToken)
        {
            var result = await _categoryService.UpdateAsync(category, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteCategory)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteCategory(Guid categoryId, CancellationToken cancellationToken)
        {
            return await _categoryService.DeleteAsync(categoryId, true, true, true, cancellationToken);
        }

    }
}
