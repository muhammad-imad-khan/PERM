namespace Perm.Core.ExceptionManager
{
    public class PermApiException : PermException
    {
        public PermApiException(ResponseCode code, params object[] param) : base(code, param)
        {
        }
    }
}