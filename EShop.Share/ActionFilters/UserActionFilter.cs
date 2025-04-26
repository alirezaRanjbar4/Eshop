using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Share.ActionFilters
{
    public class UserActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<UserActionFilter> _logger;
        public UserActionFilter(ILogger<UserActionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            var resonseReq = await GetRequestAsTextAsync(context.HttpContext.Request);
            _logger.LogInformation(resonseReq.Item1, resonseReq.Item2);
            var result = await next();

            // execute any code after the action executes
        }

        private async Task<Tuple<string, Dictionary<string, string>>> GetRequestAsTextAsync(HttpRequest httpRequest)
        {
            var request = httpRequest;
            var route = request.Path.HasValue ? request.Path.Value : "";
            var requestHeader = request.Headers.Aggregate("", (current, header) => current + $"{header.Key}: {header.Value}{Environment.NewLine}");
            var requestBody = "";
            request.EnableBuffering();
            using (var stream = new StreamReader(request.Body))
            {
                stream.BaseStream.Position = 0;
                requestBody = await stream.ReadToEndAsync();
            }

            var properties = new Dictionary<string, string>()
                 {
                    { "Route", route},
                    { "Method", request.Method},
                    { "RequestBody", requestBody }
                 };

            return new Tuple<string, Dictionary<string, string>>(route, properties);
        }
    }
}
