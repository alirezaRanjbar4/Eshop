using Eshop.DTO.Identities.DynamicAccess;
using Eshop.Resource.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Eshop.Service.Security
{
    public class MvcActionsDiscoveryService : IMvcActionsDiscoveryService
    {
        private readonly ConcurrentDictionary<string, Lazy<ICollection<ControllerViewModel>>> _allSecuredActionsWithPloicy = new ConcurrentDictionary<string, Lazy<ICollection<ControllerViewModel>>>();
        private readonly IStringLocalizer<PermissionResource> _localizer;
        public ICollection<ControllerViewModel> MvcControllers { get; }


        public MvcActionsDiscoveryService(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IStringLocalizer<PermissionResource> localizer)
        {

            _localizer = localizer;
            var culture = "fa-IR";
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            if (actionDescriptorCollectionProvider == null)
            {
                throw new ArgumentNullException(nameof(actionDescriptorCollectionProvider));
            }

            MvcControllers = new List<ControllerViewModel>();
            var lastControllerName = string.Empty;
            ControllerViewModel currentController = null;

            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            foreach (var actionDescriptor in actionDescriptors)
            {
                if (!(actionDescriptor is ControllerActionDescriptor descriptor))
                {
                    continue;
                }

                var controllerTypeInfo = descriptor.ControllerTypeInfo;
                var actionMethodInfo = descriptor.MethodInfo;

                if (lastControllerName != descriptor.ControllerName)
                {
                    currentController = new ControllerViewModel
                    {
                        AreaName = controllerTypeInfo.GetCustomAttribute<AreaAttribute>()?.RouteValue,
                        ControllerAttributes = GetAttributes(controllerTypeInfo),
                        ControllerDisplayName = !string.IsNullOrEmpty(controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName) ? _localizer[controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName] : controllerTypeInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                        ControllerName = descriptor.ControllerName,
                    };
                    MvcControllers.Add(currentController);

                    lastControllerName = descriptor.ControllerName;
                }

                currentController?.MvcActions.Add(new ActionDTO
                {
                    ControllerId = currentController.ControllerId,
                    ActionName = descriptor.ActionName,
                    ActionDisplayName = !string.IsNullOrEmpty(actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName) ? _localizer[actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName] : actionMethodInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName,
                    ActionAttributes = GetAttributes(actionMethodInfo),
                    IsSecuredAction = IsSecuredAction(controllerTypeInfo, actionMethodInfo)
                });
            }
        }

        public ICollection<ControllerViewModel> GetAllSecuredControllerActionsWithPolicy(string policyName)
        {
            var getter = _allSecuredActionsWithPloicy.GetOrAdd(policyName, y => new Lazy<ICollection<ControllerViewModel>>(
                () =>
                {
                    var controllers = new List<ControllerViewModel>(MvcControllers);
                    foreach (var controller in controllers)
                    {
                        controller.MvcActions = controller.MvcActions.Where(
                            model => model.IsSecuredAction &&
                            (
                            model.ActionAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName ||
                            controller.ControllerAttributes.OfType<AuthorizeAttribute>().FirstOrDefault()?.Policy == policyName
                            )).ToList();
                    }
                    return controllers.Where(model => model.MvcActions.Any()).ToList();
                }));
            return getter.Value;
        }

        private static List<Attribute> GetAttributes(MemberInfo actionMethodInfo)
        {
            return actionMethodInfo.GetCustomAttributes(inherit: true)
                                   .Where(attribute =>
                                   {
                                       var attributeNamespace = attribute.GetType().Namespace;
                                       return attributeNamespace != typeof(CompilerGeneratedAttribute).Namespace &&
                                              attributeNamespace != typeof(DebuggerStepThroughAttribute).Namespace;
                                   })
                                    .Cast<Attribute>()
                                   .ToList();
        }

        private static bool IsSecuredAction(MemberInfo controllerTypeInfo, MemberInfo actionMethodInfo)
        {
            var actionHasAllowAnonymousAttribute = actionMethodInfo.GetCustomAttribute<AllowAnonymousAttribute>(inherit: true) != null;
            if (actionHasAllowAnonymousAttribute)
            {
                return false;
            }

            var controllerHasAuthorizeAttribute = controllerTypeInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (controllerHasAuthorizeAttribute)
            {
                return true;
            }

            var actionMethodHasAuthorizeAttribute = actionMethodInfo.GetCustomAttribute<AuthorizeAttribute>(inherit: true) != null;
            if (actionMethodHasAuthorizeAttribute)
            {
                return true;
            }

            return false;
        }

    }
}
