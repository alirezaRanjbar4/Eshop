namespace Eshop.DTO.Identities.DynamicAccess
{
    public class ControllerDTO
    {
        public string AreaName { get; set; }
        public string ControllerDisplayName { get; set; }
        public string ControllerName { get; set; }
        public string ControllerId { get; set; }

        public IList<ActionDetailsDTO> ActionDetails { get; set; } = new List<ActionDetailsDTO>();
    }
}
