using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Enum;
using Eshop.Common.Helpers.Resource;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.Resource.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;

namespace Eshop.Common.ActionFilters;

public class SuccessFilterAttribute : ActionFilterAttribute
{
    private IStringLocalizer? _localizer;
    private IStringLocalizerFactory _factory;
    public ResourceKeyResult ResourceKeyResult { get; private set; }
    public string EnumValue;
    private System.Enum EnumType;
    public ResultType ResultType { get; set; }
    public object ResourceKey
    {
        get
        {
            return EnumType;
        }
        set
        {
            EnumType = (System.Enum)System.Enum.Parse(value.GetType(), value.ToString());
            EnumValue = value.ToString();
        }
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageType.fa_IR.ToString());
        _factory = context.HttpContext.RequestServices.GetRequiredService<IStringLocalizerFactory>();

        if (_factory != null)
        {
            var resourceName = GetResourceType(EnumType.GetType().Name);
            _localizer = _factory.Create(resourceName);
            EnumValue = _localizer[EnumValue];
        }

        ResourceKeyResult = new ResourceKeyResult(ResultType);
        ResourceKeyResult.ResourceKeyList.Add(new ResourceKey(EnumType.GetType().Name, EnumValue));

        var objResult = context.Result as JsonResult;

        if (objResult != null)
        {
            var statusCode = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "StatusCode");
            var data = Utility.ObjectProvider.GetPropertyValue(objResult.Value, "Data");
            if (data != null && (HttpStatusCode)statusCode == HttpStatusCode.OK)
            {
                var apiResult = new ApiResult<object>(HttpStatusCode.OK, Utility.ObjectProvider.GetPropertyValue(objResult.Value, "Data"), Utility.ObjectProvider.GetPropertyValue(objResult.Value, "Message").ToString(), ResourceKeyResult);
                context.Result = new JsonResult(apiResult);
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