namespace Perm.Core.ExceptionManager
{
    public class PermException : Exception
    {
        private readonly int _code = -1;
        protected string _message;
        public readonly object[] Param;
        public string ResponseCode;
        public Exception OriginalException;

        protected PermException(string message) : base(message)
        {
            _message = message;
        }

        protected PermException(int code)
        {
            _code = code;
        }

        protected PermException(int responseCode, params object[] param)
        {
            ResponseCode = responseCode.ToString();
            Param = param;
        }

        protected PermException(ResponseCode responseCode, params object[] param)
        {
            ResponseCode = responseCode;
            Param = param;
        }

        public override string Message => ConvertCodeToFriendlyMessage(_code);

        private string ConvertCodeToFriendlyMessage(int code)
        {
            return _message;
        }
    }
}