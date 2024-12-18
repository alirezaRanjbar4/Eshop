using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.Core;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.General;
using Eshop.DTO.Identities.DynamicAccess;
using Eshop.DTO.Models;
using Eshop.Enum;
using Eshop.Service.Models.Product;
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
    [DisplayName("Product")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet(nameof(GetAllProduct))]
        public async Task<List<ProductDTO>> GetAllProduct(CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync<ProductDTO>(null, null, null, false, cancellationToken);
        }


        [HttpPost(nameof(GetAllProductWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<ProductDTO>>> GetAllProductWithTotal(BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsyncWithTotal<ProductDTO>(
                searchDTO,
                x => string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm),
                null,
                o => o.OrderByDescending(x => x.CreateDate),
                false, cancellationToken);
        }


        [HttpPost(nameof(AddProduct)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddProduct([FromBody] ProductDTO product, CancellationToken cancellationToken)
        {
            var result = await _productService.AddAsync(product, true, cancellationToken);
            return result != null;
        }


        [HttpPost(nameof(UpdateProduct)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateProduct([FromBody] ProductDTO product, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(product, true, true, cancellationToken);
            return result != null;
        }


        [HttpDelete(nameof(DeleteProduct)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        [Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await _productService.DeleteAsync(productId, true, true, true, cancellationToken);
        }

    }
}
