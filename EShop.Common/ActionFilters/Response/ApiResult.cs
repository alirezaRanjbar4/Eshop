using Eshop.Common.Helpers.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace Eshop.Common.ActionFilters.Response
{
    public class ApiResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public ResourceKeyResult? ResourceKeyResult { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        public int TotalRecord { get; set; }




        public ApiResult(HttpStatusCode statusCode, string message = null, ResourceKeyResult resourceKeyResult = null, int totalRecord = 0)
        {

            StatusCode = statusCode;
            Message = message ?? statusCode.ToString();
            ResourceKeyResult = resourceKeyResult;
            TotalRecord = totalRecord;


        }

        #region Implicit Operators
        public static implicit operator ApiResult(OkResult result)
        {
            return new ApiResult(HttpStatusCode.OK);
        }

        public static implicit operator ApiResult(BadRequestResult result)
        {
            return new ApiResult(HttpStatusCode.BadRequest);
        }

        public static implicit operator ApiResult(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult(HttpStatusCode.BadRequest, message);
        }

        public static implicit operator ApiResult(ContentResult result)
        {
            return new ApiResult(HttpStatusCode.OK, result.Content);
        }

        public static implicit operator ApiResult(NotFoundResult result)
        {
            return new ApiResult(HttpStatusCode.NotFound);
        }
        #endregion
    }

    public class ApiResult<TData> : ApiResult
        where TData : class
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TData Data { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }

        public ApiResult(HttpStatusCode statusCode, TData data, string message = null, ResourceKeyResult ResourceKeyResult = null, int totalRecord = 0, IEnumerable<IdentityError> errors = null)
            : base(statusCode, message, ResourceKeyResult, totalRecord)
        {
            Data = data;
            Errors = errors;
        }

        #region Implicit Operators
        public static implicit operator ApiResult<TData>(TData data)
        {
            return new ApiResult<TData>(HttpStatusCode.OK, data);
        }

        public static implicit operator ApiResult<TData>(OkResult result)
        {
            return new ApiResult<TData>(HttpStatusCode.OK, null);
        }

        public static implicit operator ApiResult<TData>(OkObjectResult result)
        {
            return new ApiResult<TData>(HttpStatusCode.OK, (TData)result.Value);
        }

        public static implicit operator ApiResult<TData>(BadRequestResult result)
        {
            return new ApiResult<TData>(HttpStatusCode.BadRequest, null);
        }

        public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
        {
            var message = result.Value.ToString();
            if (result.Value is SerializableError errors)
            {
                var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                message = string.Join(" | ", errorMessages);
            }
            return new ApiResult<TData>(HttpStatusCode.BadRequest, null, message);
        }

        public static implicit operator ApiResult<TData>(ContentResult result)
        {
            return new ApiResult<TData>(HttpStatusCode.OK, null, result.Content);
        }

        public static implicit operator ApiResult<TData>(NotFoundResult result)
        {
            return new ApiResult<TData>(HttpStatusCode.NotFound, null);
        }

        public static implicit operator ApiResult<TData>(NotFoundObjectResult result)
        {
            return new ApiResult<TData>(HttpStatusCode.NotFound, (TData)result.Value);
        }
        #endregion
    }


}
