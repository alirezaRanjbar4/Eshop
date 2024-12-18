using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Net;

namespace Eshop.Common.Exceptions
{
    public class IdentityExcetion : IdentityError
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public IEnumerable<IdentityError> Errors { get; set; }
        public IdentityExcetion(IEnumerable<IdentityError> errs, string message = null)
        {
            StatusCode = HttpStatusCode.NotFound;
            Message = message;
            Errors = errs;
        }

    }
}
