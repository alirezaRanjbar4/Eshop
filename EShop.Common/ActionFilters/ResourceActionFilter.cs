using Eshop.Common.Enum;
using Eshop.Common.Helpers.Resource;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.Resource.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;

namespace Eshop.Common.ActionFilters;

public class ResourceActionFilter : ActionFilterAttribute
{
    private IStringLocalizer? _localizer;
    private IStringLocalizerFactory _factory;
    public ResourceActionFilter(IStringLocalizerFactory factory)
    {
        _factory = factory;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        List<string> resultStatusMessage = new List<string>();
        var objResult = context.Result as JsonResult;

        if (objResult != null && objResult.Value != null)
        {
            var statusCode = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "StatusCode");
            statusCode = statusCode ?? Utility.ObjectProvider.GetPropertyValue(objResult, "StatusCode");
            var ResourceKeyResult = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "ResourceKeyResult");
            if (statusCode != null && (HttpStatusCode)statusCode == HttpStatusCode.Unauthorized)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            if (statusCode != null && ResourceKeyResult != null)
            {
                var resultStatusList = (List<ResourceKey>)Utility.ObjectProvider.GetPropertyValue(ResourceKeyResult, "ResourceKeyList");

                if (resultStatusList != null)
                {
                    //var routingValues = context.RouteData.Values.Values.ToList();
                    //var currentArea = routingValues[0].ToString() ?? string.Empty;
                    // افزودن مقدار به CurrentUICulture
                    var culture = "fa-IR";
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

                    foreach (var item in resultStatusList)
                    {
                        var resourceType = GetResourceType(item.EnumName);
                        if (resourceType != null)
                        {
                            _localizer = _factory.Create(resourceType);

                            foreach (var notification in item.ResourceKeys)
                            {
                                var result = _localizer[notification];
                                resultStatusMessage.Add(result);
                            }
                        }
                    }

                    objResult.Value = new
                    {
                        Total = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "Total"),
                        ResourceKeyResult = new { ResultStatusList = resultStatusMessage, ResultType = Utility.ObjectProvider.GetPropertyValue(ResourceKeyResult, "ResultType") },
                        StatusCode = statusCode,
                        Caller = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "Caller"),
                        UIResourceList = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "UIResourceList"),
                        Data = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "Data"),
                        OperationState = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "OperationState")
                    };

                    context.Result = objResult as IActionResult;
                }
            }
        }
        base.OnResultExecuting(context);
    }

    private Type GetResourceType(string controllerName)
    {
        var assemblyResource = typeof(GlobalResource).Assembly;
        Type? communicatorType = assemblyResource.GetTypes().FirstOrDefault(x => x.IsClass && x.Name.Equals(nameof(GlobalResource)));
        if (!controllerName.Equals(nameof(GlobalResourceEnums)))
        {
            var resourceName = $"{controllerName}Resource";
            communicatorType = assemblyResource.GetTypes().FirstOrDefault(x => x.IsClass && x.Name.Equals(resourceName));
        }

        if (communicatorType == null)
            return null!;
        return communicatorType!;
    }
}