using AutoMapper;
using Eshop.Application.DTO.Models.Service;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Infrastructure.Repository.General;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Application.Service.Models.Service
{
    public class ServiceService : BaseService<ServiceEntity>, IServiceService
    {
        private readonly IBaseService<ServicePriceEntity> _servicePriceService;
        private readonly IBaseService<ServiceCategoryEntity> _serviceCategoryService;
        public ServiceService(
            IBaseService<ServicePriceEntity> servicePriceService,
            IBaseService<ServiceCategoryEntity> serviceCategoryService,
            IMapper mapper,
            IBaseRepository<ServiceEntity> ServiceRepository) : base(ServiceRepository, mapper)
        {
            _servicePriceService = servicePriceService;
            _serviceCategoryService = serviceCategoryService;
        }

        public async Task<bool> AddService(ServiceDTO service, CancellationToken cancellationToken)
        {
            var addResult = await AddAsync(service, true, cancellationToken);
            if (addResult != null)
            {
                await _servicePriceService.AddAsync(new ServicePriceDTO() { Price = service.Price, ServiceId = addResult.Id }, true, cancellationToken);

                foreach (var categoryId in service.ServiceCategoryIds)
                {
                    await _serviceCategoryService.AddAsync(new ServiceCategoryDTO() { CategoryId = categoryId, ServiceId = addResult.Id }, true, cancellationToken);
                }
                return true;
            }


            return false;
        }

        public async Task<bool> UpdateService(ServiceDTO service, CancellationToken cancellationToken)
        {
            var findResult = await GetAsync<ServiceDTO>(x => x.Id == service.Id, i => i.Include(x => x.ServicePrices).Include(x => x.ServiceCategories), false, cancellationToken);
            if (findResult == null)
                return false;

            var editResult = await UpdateAsync(service, true, true, cancellationToken);
            if (editResult != null)
            {
                if (findResult.Price != service.Price)
                {
                    var prices = await _servicePriceService.GetAllAsync<ServicePriceDTO>(
                        x => x.ServiceId == service.Id,
                        null,
                        o => o.OrderByDescending(x => x.CreateDate),
                        false,
                        cancellationToken);

                    if (prices != null && prices.Any())
                    {
                        var lastPrice = prices.First();
                        lastPrice.ExpiryDate = DateTime.UtcNow;
                        await _servicePriceService.UpdateAsync(lastPrice, true, true, cancellationToken);
                    }
                    await _servicePriceService.AddAsync(new ServicePriceDTO() { Price = service.Price, ServiceId = service.Id }, true, cancellationToken);
                }


                var addList = service.ServiceCategoryIds.Where(x => !findResult.ServiceCategoryIds.Contains(x));
                foreach (var categoryId in addList)
                {
                    await _serviceCategoryService.AddAsync(new ServiceCategoryDTO() { CategoryId = categoryId, ServiceId = service.Id }, true, cancellationToken);
                }

                var deleteList = findResult.ServiceCategoryIds.Where(x => !service.ServiceCategoryIds.Contains(x));
                foreach (var categoryId in deleteList)
                {
                    var item = await _serviceCategoryService.GetAsync<ServiceCategoryDTO>(x => x.ServiceId == service.Id && x.CategoryId == categoryId, null, false, cancellationToken);
                    await _serviceCategoryService.DeleteAsync(item.Id, true, true, true, cancellationToken);
                }

                return true;
            }

            return false;
        }
    }
}
