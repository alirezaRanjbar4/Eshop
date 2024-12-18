using Eshop.DTO.Identities.DynamicAccess;

namespace Eshop.Service.Security
{
    public interface IMvcActionsDiscoveryService
    {
        ICollection<ControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }
}
