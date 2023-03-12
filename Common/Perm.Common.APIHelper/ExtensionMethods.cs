using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Perm.Common.APIHelper
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Get header value from IHttpContextAccessor
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpContextAccessor"></param>
        /// <param name="headerName"></param>
        /// <returns></returns>
        public static T GetHeader<T>(this IHttpContextAccessor httpContextAccessor, string headerName) where T : IConvertible
        {
            if (httpContextAccessor.HttpContext != null)
            {
                StringValues requestHeader = httpContextAccessor.HttpContext.Request.Headers[headerName];
                return requestHeader.ParseTo<T>();

            }

            return default(T);
        }
    }
}