using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.ServiceCategory
{
    public class ServiceCategoryRepository : BaseRepository<ServiceCategoryEntity>, IServiceCategoryRepository
    {
        public ServiceCategoryRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
