﻿using AutoMapper;
using Eshop.Application.DTO.Models.Product;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Service.Models.Product
{
    public class ProductService : BaseService<ProductEntity>, IProductService
    {
        private readonly IBaseService<ProductPriceEntity> _productPriceService;
        private readonly IBaseService<ProductCategoryEntity> _productCategoryService;
        public ProductService(
            IMapper mapper,
            IBaseRepository<ProductEntity> ProductRepository,
            IBaseService<ProductPriceEntity> productPriceService,
            IBaseService<ProductCategoryEntity> productCategoryService) : base(ProductRepository, mapper)
        {
            _productPriceService = productPriceService;
            _productCategoryService = productCategoryService;
        }

        public async Task<bool> AddProduct(ProductDTO product, CancellationToken cancellationToken)
        {
            var addResult = await AddAsync(product, true, cancellationToken);
            if (addResult != null)
            {
                await _productPriceService.AddAsync(new ProductPriceDTO() { Price = product.Price, ProductId = addResult.Id }, true, cancellationToken);

                foreach (var categoryId in product.ProductCategoryIds)
                {
                    await _productCategoryService.AddAsync(new ProductCategoryDTO() { CategoryId = categoryId, ProductId = addResult.Id }, true, cancellationToken);
                }
                return true;
            }


            return false;
        }

        public async Task<bool> UpdateProduct(ProductDTO product, CancellationToken cancellationToken)
        {
            var findResult = await GetAsync<ProductDTO>(x => x.Id == product.Id, i => i.Include(x => x.ProductPrices).Include(x => x.ProductCategories), false, cancellationToken);
            if (findResult == null)
                return false;

            var editResult = await UpdateAsync(product, true, true, cancellationToken);
            if (editResult != null)
            {
                if (findResult.Price != product.Price)
                {
                    var prices = await _productPriceService.GetAllAsync<ProductPriceDTO>(
                        x => x.ProductId == product.Id,
                        null,
                        o => o.OrderByDescending(x => x.CreateDate),
                        false,
                        cancellationToken);

                    if (prices != null && prices.Any())
                    {
                        var lastPrice = prices.First();
                        lastPrice.ExpiryDate = DateTime.UtcNow;
                        await _productPriceService.UpdateAsync(lastPrice, true, true, cancellationToken);
                    }
                    await _productPriceService.AddAsync(new ProductPriceDTO() { Price = product.Price, ProductId = product.Id }, true, cancellationToken);
                }


                var addList = product.ProductCategoryIds.Where(x => !findResult.ProductCategoryIds.Contains(x));
                foreach (var categoryId in addList)
                {
                    await _productCategoryService.AddAsync(new ProductCategoryDTO() { CategoryId = categoryId, ProductId = product.Id }, true, cancellationToken);
                }

                var deleteList = findResult.ProductCategoryIds.Where(x => !product.ProductCategoryIds.Contains(x));
                foreach (var categoryId in deleteList)
                {
                    var item = await _productCategoryService.GetAsync<ProductCategoryDTO>(x => x.ProductId == product.Id && x.CategoryId == categoryId, null, false, cancellationToken);
                    await _productCategoryService.DeleteAsync(item.Id, true, true, true, cancellationToken);
                }

                return true;
            }

            return false;
        }
    }
}
