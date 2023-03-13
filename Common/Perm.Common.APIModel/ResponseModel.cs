namespace Perm.Common.APIModel
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string Code { get; set; } = "0000";
        public string Message { get; set; } = "Your request successfully completed";
        public bool IsError => Code != "0000";
        public object AdditionalInfo { get; set; }
        public bool IsStaticContent { get; set; }
    }
}