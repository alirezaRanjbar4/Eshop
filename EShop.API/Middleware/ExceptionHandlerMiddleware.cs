using Eshop.Common.ActionFilters.Response;
using Eshop.Common.Exceptions;
using Eshop.Common.Helpers.Resource;
using Eshop.Enum;
using Eshop.Resource.Global;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Eshop.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly IHostingEnvironment _env;
        private IStringLocalizer _localizer;
        private IStringLocalizerFactory _factory;
        public ExceptionHandlerMiddleware(RequestDelegate next, IHostingEnvironment env, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            string message = null;
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;

            try
            {
                await _next(context);
            }
            catch (UnAuthrizedException exception)
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    //["StackTrace"] = exception.StackTrace,
                };
                // _logger.LogError(exception, exception.Message);
                httpStatusCode = exception.HttpStatus;
                message = exception.Message;
               // _logger.ErrorLoggerProvider(message, dic, exception);

                SetUnAuthorizeResponse(message, exception);
            }
            catch (AppException exception)
            {


                // _logger.LogError(exception, exception.Message);
                httpStatusCode = exception.HttpStatus;
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace,
                };

                if (_env.IsDevelopment())
                {
                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }
                    if (exception.AdditionalData != null)
                        dic.Add("AdditionalData", JsonConvert.SerializeObject(exception.AdditionalData));

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }

               // _logger.ErrorLoggerProvider(message, dic, exception);
                await WriteToResponseAsync(new ResourceKeyResult { ResultType = ResultType.Error, ResourceKeyList = null });
            }
            catch (UiValidationException exception)
            {
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = "",
                };

                if (_env.IsDevelopment())
                {

                    if (exception.InnerException != null)
                    {
                        dic.Add("InnerException.Exception", exception.InnerException.Message);
                        dic.Add("InnerException.StackTrace", exception.InnerException.StackTrace);
                    }

                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }

                //_logger.ErrorLoggerProvider(exception.Message, dic, exception);
                await WriteToResponseUiValidationAsync(message, exception.OperationState);
            }
            catch (SecurityTokenExpiredException exception)
            {
                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception.Message, exception);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);
                SetUnAuthorizeResponse(exception.Message, exception);
                await WriteToResponseAsync(new ResourceKeyResult { ResultType = ResultType.Error, ResourceKeyList = null });
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                var dic = new Dictionary<string, string>
                {
                    ["Exception"] = exception.Message,
                    ["StackTrace"] = exception.StackTrace,
                };
                if (_env.IsDevelopment())
                {
                    message = JsonConvert.SerializeObject(dic);
                }
                else
                {
                    message = exception.Message;
                }
                //_logger.ErrorLoggerProvider(exception.Message, dic, exception);
                await WriteToResponseAsync(new ResourceKeyResult { ResultType = ResultType.Error, ResourceKeyList = null });
            }

            async Task WriteToResponseAsync(ResourceKeyResult operationState)
            {
                if (context.Response.HasStarted)
                    throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                var result = new ApiResult(httpStatusCode, message, operationState);
                var json = JsonConvert.SerializeObject(result);

                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }


            async Task WriteToResponseUiValidationAsync(string message, ResourceKeyResult operationState)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageType.fa_IR.ToString());
                _factory = context.RequestServices.GetRequiredService<IStringLocalizerFactory>();

                if (operationState != null)
                {
                    foreach (var item in operationState.ResourceKeyList)
                    {
                        var resourceType = GetResourceType(item.EnumName);
                        if (resourceType != null)
                        {
                            var translatedResourceKeys = new List<string>();
                            _localizer = _factory.Create(resourceType);
                            for (int i = 0; i < item.ResourceKeys.Count; i++)
                            {
                                var localizer = _localizer[item.ResourceKeys.ElementAt(i)];
                                translatedResourceKeys.Add(localizer);
                            }
                            item.ResourceKeys = translatedResourceKeys;
                        }
                    }
                    if (context.Response.HasStarted)
                        throw new InvalidOperationException("The response has already started, the http status code middleware will not be executed.");

                    var result = new ApiResult(httpStatusCode, message, operationState);
                    var json = JsonConvert.SerializeObject(result);

                    context.Response.StatusCode = (int)httpStatusCode;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(json);
                }
            }

            Type GetResourceType(string controllerName)
            {
                var assemblyResource = typeof(GlobalResource).Assembly;
                Type communicatorType = assemblyResource.GetTypes().FirstOrDefault(x => x.IsClass && x.Name.Equals(nameof(GlobalResource)));
                if (!controllerName.Equals(nameof(GlobalResourceEnums)))
                {
                    var resourceName = $"{controllerName}Resource";
                    communicatorType = assemblyResource.GetTypes().FirstOrDefault(x => x.IsClass && x.Name.Equals(resourceName));
                }

                if (communicatorType == null)
                    return null!;
                return communicatorType!;
            }


            void SetUnAuthorizeResponse(string message, Exception exception)
            {
                httpStatusCode = HttpStatusCode.Unauthorized;
                var result = new ApiResult(httpStatusCode, message);
                var json = JsonConvert.SerializeObject(result);
                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                context.Response.WriteAsync(json);

            }
        }
    }
}


