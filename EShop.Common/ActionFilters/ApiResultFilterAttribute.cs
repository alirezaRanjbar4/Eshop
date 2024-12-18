using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Helpers.Utilities.Utilities;
using Eshop.Common.Helpers.Utilities.Utilities.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Eshop.Common.ActionFilters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            switch (context.Result)
            {
                case OkObjectResult okObjectResult:
                    {
                        var apiResult = new ApiResult<object>(HttpStatusCode.OK, okObjectResult.Value);
                        var dataResult = new ObjectHelper().GetPropertyValue(okObjectResult.Value!, "Data");
                        if (dataResult != null)
                        {

                            int total = Convert.ToInt32(new ObjectHelper().GetPropertyValue(okObjectResult.Value!, "TotalRecord"));
                            var error = new ObjectHelper().GetPropertyValue(okObjectResult.Value!, "Errors");
                            var message = new ObjectHelper().GetPropertyValue(okObjectResult.Value!, "Message");

                            apiResult = new ApiResult<object>(HttpStatusCode.OK, dataResult, message.ToString(), null, total, error as IEnumerable<IdentityError>);
                        }

                        context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
                        break;
                    }

                case OkResult okResult:
                    {
                        var apiResult = new ApiResult(HttpStatusCode.OK);
                        context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };
                        break;
                    }

                case BadRequestResult badRequestResult:
                    {

                        var apiResult = new ApiResult(HttpStatusCode.BadRequest);
                        context.Result = new JsonResult(apiResult) { StatusCode = badRequestResult.StatusCode };
                        break;
                    }

                case BadRequestObjectResult badRequestObjectResult:
                    {
                        var message = badRequestObjectResult.Value.ToString();
                        if (badRequestObjectResult.Value is SerializableError serializableErrors)
                        {
                            var errorMessages = serializableErrors.SelectMany(p => (string[])p.Value).Distinct();
                            message = string.Join(" | ", errorMessages);
                        }
                        if (badRequestObjectResult.Value is ValidationProblemDetails errors)
                        {
                            var errorMessages = errors.Errors.SelectMany(p => p.Value).Distinct();
                            message = string.Join(" | ", errorMessages);
                        }

                        var apiResult = new ApiResult(HttpStatusCode.BadRequest, message);
                        context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode };
                        break;
                    }

                case ContentResult contentResult:
                    {
                        var apiResult = new ApiResult(HttpStatusCode.OK, contentResult.Content);
                        context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode };
                        break;
                    }

                case NotFoundResult notFoundResult:
                    {
                        var apiResult = new ApiResult(HttpStatusCode.NotFound);
                        context.Result = new JsonResult(apiResult) { StatusCode = notFoundResult.StatusCode };
                        break;
                    }

                case NotFoundObjectResult notFoundObjectResult:
                    {
                        var apiResult = new ApiResult<object>(HttpStatusCode.NotFound, notFoundObjectResult.Value);
                        context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode };
                        break;
                    }

                case ObjectResult objectResult when objectResult.StatusCode == null && !(objectResult.Value is ApiResult):
                    {

                        var apiResult = new ApiResult<object>(HttpStatusCode.OK, objectResult.Value == null ? new object() : objectResult.Value);
                        object? message = null;
                        object? error = null;

                        var dataResult = Utility.ObjectProvider.GetPropertyValue(objectResult.Value!, "Data");
                        var statuseCode = Utility.ObjectProvider.GetPropertyValue(objectResult.Value!, "StatusCode");
                        message = Utility.ObjectProvider.GetPropertyValue(objectResult.Value!, "Message");
                        if (dataResult != null)
                        {
                            int total = Convert.ToInt32(Utility.ObjectProvider.GetPropertyValue(objectResult.Value!, "TotalRecords"));

                            if (Utility.ObjectProvider.GetPropertyValue(dataResult!, "TotalRecords") != null)
                            {
                                total = Convert.ToInt32(Utility.ObjectProvider.GetPropertyValue(dataResult!, "TotalRecords"));

                            }
                            if (Utility.ObjectProvider.GetPropertyValue(dataResult!, "Errors") != null)
                            {
                                error = Utility.ObjectProvider.GetPropertyValue(dataResult!, "Errors");

                            }
                            if (Utility.ObjectProvider.GetPropertyValue(dataResult!, "Message") != null)
                            {
                                message = Utility.ObjectProvider.GetPropertyValue(dataResult!, "Message");
                            }

                            apiResult = new ApiResult<object>(HttpStatusCode.OK, dataResult, message != null ? message.ToString() : null, null, total, error != null ? error as IEnumerable<IdentityError> : null);
                        }


                        context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode };
                        break;
                    }
            }

            base.OnResultExecuting(context);
        }
    }
}