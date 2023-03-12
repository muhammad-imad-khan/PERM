namespace Perm.Common.APIModel
{
    public interface IRequestModel
    {
    }

    public class RequestModelBase : IRequestModel
    {
        /// <summary>
        /// Raw request body from http POST /PUT
        /// </summary>
        public string RequestBodyRaw { get; set; }
    }
}