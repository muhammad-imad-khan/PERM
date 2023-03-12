using Microsoft.Extensions.DependencyInjection;

namespace Perm.Common
{
    /// <summary>
    /// Application wide constant class to hold the constant variables
    /// </summary>
    public static class Constant
    {
        /// <summary>
        /// Application wide date format
        /// </summary>
        public static readonly string JSON_DATE_TIME_FORMAT = "yyyy-MM-ssThh:mm:ss.fff";
        public static readonly string SESSIONID_FORMAT = "yyMMddHHmmssffff";

        /// <summary>
        /// 
        /// </summary>
        public static List<ServiceDescriptor> ServiceDescriptors = new List<ServiceDescriptor>();

        /// <summary>
        /// default pagination size
        /// </summary>
        public const int DEFAULT_PAGE_ROW_COUNT = 2000;
    }
}