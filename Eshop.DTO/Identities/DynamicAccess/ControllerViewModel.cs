using System.Text.Json.Serialization;

namespace Eshop.DTO.Identities.DynamicAccess
{
    public class ControllerViewModel
    {
        public string AreaName { get; set; }
        public IList<Attribute> ControllerAttributes { get; set; }
        public string ControllerDisplayName { get; set; }
        private string _controllerName;
        private string _controllerId;

        public string ControllerName
        {
            get => _controllerName;
            set
            {
                _controllerName = value;
                // هر بار که ControllerName تغییر می‌کند، ControllerId نیز به‌روزرسانی شود.
                _controllerId = $"{AreaName}:{_controllerName}";
            }
        }

        public string ControllerId => _controllerId;

        public IList<ActionDTO> MvcActions { get; set; } = new List<ActionDTO>();
    }

}
