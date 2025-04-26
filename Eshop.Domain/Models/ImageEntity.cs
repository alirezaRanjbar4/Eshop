using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class ImageEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string URL { get; set; }

        public Guid? ProductId { get; set; }
        public virtual ProductEntity? Product { get; set; }
    }
}