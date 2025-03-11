using Eshop.Common.Helpers.Utilities.Interface;
using Eshop.DTO.Models.Service;
using Eshop.Entity.Models;
using Eshop.Service.General;

namespace Eshop.Service.Models.Service
{
    public interface IServiceService : IBaseService<ServiceEntity>, IScopedDependency
    {
        Task<bool> AddService(ServiceDTO Service, CancellationToken cancellationToken);
        Task<bool> UpdateService(ServiceDTO Service, CancellationToken cancellationToken);
    }
}

