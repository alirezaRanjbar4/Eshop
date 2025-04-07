using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.DemoRequest;
using Eshop.Service.General;

namespace Eshop.Service.Models.DemoRequest
{
    public class DemoRequestService : BaseService<DemoRequestEntity>, IDemoRequestService
    {
        public DemoRequestService(IMapper mapper, IDemoRequestRepository DemoRequestRepository) : base(DemoRequestRepository, mapper)
        {
        }
    }
}
