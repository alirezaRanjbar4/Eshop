using Eshop.Entity.General;
using Eshop.Entity.Identities;

namespace Eshop.Entity.Models
{
    public class SupplierEntity : BaseTrackedModel, IBaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        //public Guid UserId { get; set; }
        //public virtual UserEntity User { get; set; }

        public Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }
    }
}