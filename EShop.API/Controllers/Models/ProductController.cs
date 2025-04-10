using Asp.Versioning;
using Eshop.Api.Components;
using Eshop.Api.Controllers.General;
using Eshop.Common.ActionFilters;
using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.DTO.General;
using Eshop.DTO.Models.Product;
using Eshop.Service.Models.Product;
using Eshop.Service.Models.ProductWarehouseLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IProductWarehouseLocationService _productWarehouseLocationService;
        public ProductController(
            IProductService productService,
            IProductWarehouseLocationService productWarehouseLocationService)
        {
            _productService = productService;
            _productWarehouseLocationService = productWarehouseLocationService;
        }


        [HttpGet(nameof(GetAllProduct))]
        public async Task<List<SimpleProductDTO>> GetAllProduct(CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync<SimpleProductDTO>(
                x => x.StoreId == CurrentUserStoreId,
                i => i.Include(x => x.ProductPrices),
                o => o.OrderBy(x => x.Name),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(GetAllProductWithTotal)), DisplayName(nameof(PermissionResourceEnums.GetAllPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<OperationResult<List<GetAllProductDTO>>> GetAllProductWithTotal([FromBody] BaseSearchDTO searchDTO, CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsyncWithTotal<GetAllProductDTO>(
                searchDTO,
                x => x.StoreId == CurrentUserStoreId && (string.IsNullOrEmpty(searchDTO.SearchTerm) || x.Name.Contains(searchDTO.SearchTerm)),
                i => i.Include(x => x.ProductCategories).ThenInclude(x => x.Category)
                      .Include(x => x.ProductWarehouseLocations)
                      .Include(x => x.ProductPrices),
                o => o.OrderByDescending(x => x.CreateDate),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(GetProduct)), DisplayName(nameof(PermissionResourceEnums.GetPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        public async Task<GetProductDTO> GetProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await _productService.GetAsync<GetProductDTO>(
                x => x.Id == productId && x.StoreId == CurrentUserStoreId,
                i => i.Include(x => x.ProductCategories)
                      .Include(x => x.ProductWarehouseLocations).ThenInclude(x => x.WarehouseLocation).ThenInclude(x => x.Warehouse)
                      .Include(x => x.ProductPrices)
                      .Include(x => x.Images),
                false,
                cancellationToken);
        }


        [HttpPost(nameof(AddProduct)), DisplayName(nameof(PermissionResourceEnums.AddPermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.AddComplete, ResultType = ResultType.Success)]
        public async Task<bool> AddProduct([FromBody] ProductDTO product, CancellationToken cancellationToken)
        {
            return await _productService.AddProduct(product, cancellationToken);
        }


        [HttpPost(nameof(UpdateProduct)), DisplayName(nameof(PermissionResourceEnums.UpdatePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.EditComplete, ResultType = ResultType.Success)]
        public async Task<bool> UpdateProduct([FromBody] ProductDTO product, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProduct(product, cancellationToken);
        }


        [HttpDelete(nameof(DeleteProduct)), DisplayName(nameof(PermissionResourceEnums.DeletePermission))]
        //[Authorize(Policy = ConstantPolicies.DynamicPermission)]
        [SuccessFilter(ResourceKey = GlobalResourceEnums.DeleteComplete, ResultType = ResultType.Success)]
        public async Task<bool> DeleteProduct(Guid productId, CancellationToken cancellationToken)
        {
            return await _productService.DeleteAsync(productId, true, true, true, cancellationToken);
        }


        [HttpGet(nameof(GetAllProductWarehouseLocation))]
        public async Task<List<SimpleDTO>> GetAllProductWarehouseLocation(Guid productId, CancellationToken cancellationToken)
        {
            return await _productWarehouseLocationService.GetAllAsync<SimpleDTO>(
                x => x.ProductId == productId,
                i => i.Include(x => x.WarehouseLocation).ThenInclude(x => x.Warehouse),
                null,
                false,
                cancellationToken);
        }

    }
}
