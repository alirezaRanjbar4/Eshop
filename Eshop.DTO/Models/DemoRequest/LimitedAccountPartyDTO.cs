using Eshop.DTO.General;

namespace Eshop.DTO.Models.DemoRequest
{
    public class LimitedDemoRequestDTO : BaseDTO
    {
        public string? Name { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? WorkBranch { get; set; }
        public string? Description { get; set; }
    }
}