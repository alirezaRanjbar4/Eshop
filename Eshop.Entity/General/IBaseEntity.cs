namespace Eshop.Entity.General;

public interface IBaseEntity
{
    Guid Id { get; set; }
    bool Deleted { get; set; }
    Guid CreateById { get; set; }
    DateTime CreateDate { get; set; }
    Guid? ModifiedById { get; set; }
    DateTime? ModifiedDate { get; set; }
}
