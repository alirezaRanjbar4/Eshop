using Eshop.Application.DTO.Models.Service;
using Eshop.Application.Service.General;
using Eshop.Domain.Models;
using Eshop.Share.Helpers.Utilities.Interface;

namespace Eshop.Application.Service.Models.Service
{
    public interface IServiceService : IBaseService<ServiceEntity>, IScopedDependency
    {
        Task<bool> AddService(ServiceDTO Service, CancellationToken cancellationToken);
        Task<bool> UpdateService(ServiceDTO Service, CancellationToken cancellationToken);
    }
}

