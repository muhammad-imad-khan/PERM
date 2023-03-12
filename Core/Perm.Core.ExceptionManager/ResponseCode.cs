namespace Perm.Core.ExceptionManager
{
    public struct ResponseCode
    {
        private string _value;

        public static implicit operator string(ResponseCode value)
        {
            return value._value;
        }

        public static implicit operator ResponseCode(string value)
        {
            ResponseCode returnValue = new ResponseCode { _value = value };
            return returnValue;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}