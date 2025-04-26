using Eshop.Share.Enum;
using Eshop.Share.Helpers.Resource;
using System;

namespace Eshop.Share.Exceptions
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
