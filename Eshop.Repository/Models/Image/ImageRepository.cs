using Eshop.Entity.Models;
using Eshop.Repository.General;
using Rasam.Data.DBContext;

namespace Eshop.Repository.Models.Image
{
    public class ImageRepository : BaseRepository<ImageEntity>, IImageRepository
    {
        public ImageRepository(ApplicationContext dataContext) : base(dataContext)
        {
        }
    }
}
