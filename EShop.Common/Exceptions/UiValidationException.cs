using Eshop.Common.Helpers.Resource;
using Eshop.Enum;
using System;

namespace Eshop.Common.Exceptions
{
    public class UiValidationException : Exception
    {
        public ResourceKeyResult OperationState { get; set; }

        public UiValidationException(ResultType statusEnum = ResultType.Success) : base()
        {
            OperationState = new ResourceKeyResult(statusEnum);
        }

    }
}
