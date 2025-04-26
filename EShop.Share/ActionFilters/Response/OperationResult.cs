using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Eshop.Share.ActionFilters.Response
{
    public class OperationResult<TData>
    {

        public OperationResult()
        {
        }

        public OperationResult(TData? data) : this()
        {

            Data = data;
        }


        public TData? Data { get; set; }
        public IEnumerable<IdentityError> Errors { get; set; }
        public string Message { get; set; } = string.Empty;
        public int TotalRecords { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}