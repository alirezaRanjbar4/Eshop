using Eshop.Application.DTO.General;

namespace Eshop.Application.DTO.Models.DemoRequest
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