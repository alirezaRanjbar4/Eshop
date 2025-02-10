using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Image
{
    public interface IImageService : IBaseService<ImageEntity>, IScopedDependency
    {
    }
}
