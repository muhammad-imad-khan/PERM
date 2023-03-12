namespace Perm.Core.ExceptionManager
{
    public class PermArgumentException : PermException
    {
        protected PermArgumentException(string message) : base(message)
        {
        }

        public PermArgumentException(string message, Exception originalException) : base(message)
        {
            OriginalException = originalException;
        }

        public PermArgumentException(int code, string message, Exception originalException) : base(code, message, originalException)
        {
            _message = message;
        }
    }
}