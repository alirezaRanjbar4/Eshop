using Eshop.Resource.Global;
using Eshop.Share.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Eshop.Presentation.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly IStringLocalizer<GlobalResource> _localizer;

    public ExceptionHandlerMiddleware(RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<ExceptionHandlerMiddleware> logger,
        IStringLocalizer<GlobalResource> localizer)
    {
        _next = next;
        _env = env;
        _logger = logger;
        _localizer = localizer;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            //throw ex;
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, title) = GetStatusAndMessage(exception);
        var detail = _env.IsDevelopment()
            ? exception.ToString()
            : null;

        var problem = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path,
            Type = $"https://httpstatuses.com/{(int)statusCode}"
        };

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonConvert.SerializeObject(problem));
    }

    private (HttpStatusCode, string) GetStatusAndMessage(Exception ex)
    {
        return ex switch
        {
            UnAuthrizedException e => (e.HttpStatus, GetMessage(e.Message)),
            AppException e => (e.HttpStatus, GetMessage(e.Message)),
            UiValidationException e => (HttpStatusCode.BadRequest, GetMessage(e.Message)),
            SecurityTokenExpiredException => (HttpStatusCode.Unauthorized, GetMessage("توکن شما منقضی شده است.")),
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, GetMessage("شما مجاز به دسترسی به این بخش نیستید.")),
            _ => (HttpStatusCode.InternalServerError, GetMessage("خطای داخلی سرور رخ داده است."))
        };
    }

    private string GetMessage(string keyOrMessage)
    {
        var localized = _localizer[keyOrMessage];
        return string.IsNullOrWhiteSpace(localized) ? keyOrMessage : localized;
    }
}
