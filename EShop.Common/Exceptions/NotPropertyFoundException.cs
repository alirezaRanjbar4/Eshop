namespace Eshop.Common.Exceptions
{
    public class NotPropertyFoundException : AppException
    {

        protected static string _message = "Not Found => ";
        public NotPropertyFoundException(string message) : base(string.Join(_message, message))
        {
        }
    }
}
