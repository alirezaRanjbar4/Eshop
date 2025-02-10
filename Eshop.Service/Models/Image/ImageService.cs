using AutoMapper;
using Eshop.Entity.Models;
using Eshop.Repository.Models.Image;
using Eshop.Service.General;

namespace Eshop.Service.Models.Image
{
    public class ImageService : BaseService<ImageEntity>, IImageService
    {
        public ImageService(IMapper mapper, IImageRepository ImageRepository) : base(ImageRepository, mapper)
        {
        }
    }
}
