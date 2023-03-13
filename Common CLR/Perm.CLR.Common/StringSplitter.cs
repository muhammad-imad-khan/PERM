using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace Perm.CLR.Common
{
    public partial class UserDefinedFunctions
    {
        [SqlFunction]
        public static SqlString StringByIndex(string str, char delimiter, int index)
        {
            if (str != null)
            {
                string[] strings = str.Split(delimiter);
                if (index >= 0 && index < strings.Length)
                    return new SqlString(strings[index]);
            }

            return SqlString.Null;
        }
    }
}