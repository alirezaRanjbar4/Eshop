using Eshop.Domain.General;

namespace Eshop.Domain.Models
{
    public class DemoRequestEntity : BaseTrackedModel, IBaseEntity
    {
        public string? Name { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? WorkBranch { get; set; }
        public string? Description { get; set; }
        public string? AdminDescription { get; set; }
        public bool IsAnswered { get; set; } = false;
    }
}