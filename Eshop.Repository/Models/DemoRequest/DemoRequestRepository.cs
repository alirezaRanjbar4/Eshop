using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.DemoRequest
{
    public class DemoRequestRepository : BaseRepository<DemoRequestEntity>, IDemoRequestRepository
    {
        public DemoRequestRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
