namespace Perm.Core.ExceptionManager
{
    public class PermSecurityException : PermException
    {
        public PermSecurityException(string message, string resultCode = null, Exception exception = null) : base(message)
        {
            ResponseCode = resultCode;
            OriginalException = exception;
        }

        public PermSecurityException(int code, params object[] param) : base(code, param)
        {
        }
    }
}