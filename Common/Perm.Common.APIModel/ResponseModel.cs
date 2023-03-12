namespace Perm.Common.APIModel
{
    //TODO: add "where" clause for generic type
    /// <summary>
    ///     This is generic model and will be return for all response.
    /// </summary>
    /// <typeparam name="T"></typeparam>
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