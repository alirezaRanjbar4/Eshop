using System;
using System.Runtime.Serialization;

namespace Eshop.Common.Exceptions
{
    [Serializable]
    internal class BaseException : Exception
    {
        private string message;
        private string v;
        private Exception ex;

        public BaseException()
        {
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BaseException(string message, string v, Exception ex)
        {
            this.message = message;
            this.v = v;
            this.ex = ex;
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
