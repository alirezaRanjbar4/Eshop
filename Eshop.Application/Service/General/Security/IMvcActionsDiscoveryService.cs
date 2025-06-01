using Eshop.Application.DTO.Identities.DynamicAccess;

namespace Eshop.Application.Service.General.Security
{
    public interface IMvcActionsDiscoveryService
    {
        ICollection<ControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }
}
