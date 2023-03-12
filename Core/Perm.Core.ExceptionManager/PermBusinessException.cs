namespace Perm.Core.ExceptionManager
{
    public class PermBusinessException : PermException
    {
        public PermBusinessException(int code) : base(code)
        {
        }

        public PermBusinessException(ResponseCode responseCode) : base(responseCode)
        {
        }

        public PermBusinessException(ResponseCode responseCode, params object[] param) : base(responseCode, param)
        {
        }
    }
}