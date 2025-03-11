using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ServicePrice
{
    public class ServicePriceRepository : BaseRepository<ServicePriceEntity>, IServicePriceRepository
    {
        public ServicePriceRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
