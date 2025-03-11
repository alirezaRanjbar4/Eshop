using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.ServiceCategory;
using Eshop.Service.General;

namespace Eshop.Service.Models.ServiceCategory
{
    public class ServiceCategoryService : BaseService<ServiceCategoryEntity>, IServiceCategoryService
    {
        public ServiceCategoryService(IMapper mapper, IServiceCategoryRepository ServiceCategoryRepository) : base(ServiceCategoryRepository, mapper)
        {
        }
    }
}
