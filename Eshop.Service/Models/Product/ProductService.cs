using AutoMapper;
using Eshop.Common.ActionFilters.Response;
using Eshop.DTO.Models;
using Eshop.DTO.Models.Product;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Product;
using Eshop.Service.General;
using Eshop.Service.Models.ProductPrice;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service.Models.Product
{
    public class ProductService : BaseService<ProductEntity>, IProductService
    {
        private readonly IProductPriceService _productPriceService;
        public ProductService(IMapper mapper, IProductRepository ProductRepository, IProductPriceService productPriceService) : base(ProductRepository, mapper)
        {
            _productPriceService = productPriceService;
        }

        public async Task<bool> AddProduct(ProductDTO product, CancellationToken cancellationToken)
        {
            var addResult = await AddAsync(product, true, cancellationToken);
            if (addResult != null)
                await _productPriceService.AddAsync(new ProductPriceDTO() { Price = product.Price, ProductId = addResult.Id }, true, cancellationToken);

            return addResult != null;
        }

        public async Task<bool> UpdateProduct(ProductDTO product, CancellationToken cancellationToken)
        {
            var findResult = await GetAsync<ProductDTO>(x => x.Id == product.Id, i => i.Include(x => x.ProductPrices), false, cancellationToken);
            if (findResult == null)
                return false;

            var editResult = await UpdateAsync(product, true, true, cancellationToken);
            if (editResult != null && findResult.Price != product.Price)
            {
                await _productPriceService.AddAsync(new ProductPriceDTO() { Price = product.Price, ProductId = product.Id }, true, cancellationToken);
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
            }

            return editResult != null;
        }
    }
}
