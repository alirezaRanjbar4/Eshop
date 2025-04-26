using Eshop.Application.DTO.Identities.DynamicAccess;

namespace Eshop.Application.Service.Security
{
    public interface IMvcActionsDiscoveryService
    {
        ICollection<ControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName);
    }
}
