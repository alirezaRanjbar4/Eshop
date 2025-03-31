using Eshop.Common.Enum;
using Eshop.Common.Helpers.Resource;
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
