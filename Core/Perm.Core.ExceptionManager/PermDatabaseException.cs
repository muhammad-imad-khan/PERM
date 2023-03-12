namespace Perm.Core.ExceptionManager
{
    public class PermDatabaseException : PermException
    {
        public PermDatabaseException(string message, string resultCode = null, Exception exception = null) : base(message)
        {
            ResponseCode = resultCode;
            OriginalException = exception;
        }

        public PermDatabaseException(int code, params object[] param) : base(code, param)
        {
        }
    }
}