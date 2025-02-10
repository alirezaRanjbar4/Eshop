using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.Entity.Models;
using Eshop.Repository.General;

namespace Eshop.Repository.Models.Image
{
    public interface IImageRepository : IBaseRepository<ImageEntity>, IScopedDependency
    {
    }
}
