using System;
using Microsoft.SqlServer.Server;

namespace Perm.CLR.Common
{
    public partial class UserDefinedFunctions
    {
        [SqlFunction]
        public static DateTime? DateTimeParse(string dateString, string dateFormat)
        {
            bool tryParseExact = DateTime.TryParseExact(dateString, dateFormat, null, System.Globalization.DateTimeStyles.None, out DateTime result);

            if (tryParseExact)
                return result;

            return null;
        }
    }
}