using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ServicePrice;
using Eshop.Service.General;

namespace Eshop.Service.Models.ServicePrice
{
    public class ServicePriceService : BaseService<ServicePriceEntity>, IServicePriceService
    {
        public ServicePriceService(IMapper mapper, IServicePriceRepository ServicePriceRepository) : base(ServicePriceRepository, mapper)
        {
        }
    }
}
